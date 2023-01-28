using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Application.Options;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Services.TwilioSms;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Twilio.Clients;

namespace Web.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services = AddOptionsServices(services);
            services = AddDIServices(services);
            services = AddNugetDIServices(services);
            services = SetupModelValidationError(services);
            return services;
        }
        private static IServiceCollection AddOptionsServices(IServiceCollection services)
        {
            // Options pattern configuration
            // Each of the options pattern setup below validates that appsettings
            // file has respective section setup as per the validation setup in the
            // options classes during the start of the application. This way we do not
            // forget to setup any configuration section.
            // App Configuration setup
            services.AddOptions<AppConfigurationOptions>()
                    .BindConfiguration("AppConfigurations")
                    .ValidateDataAnnotations()
                    .ValidateOnStart();
            // JWT token configuration
            services.AddOptions<AppJwtTokenConfigurationOptions>()
                    .BindConfiguration("JwtTokenOptions")
                    .ValidateDataAnnotations()
                    .ValidateOnStart();
            // SMTP email service setup
            
            services.AddOptions<AppEmailOptions>().
                    BindConfiguration("EmailOptions")
                    .ValidateDataAnnotations()
                    .ValidateOnStart();
            // Twilio SMS Service setup
            services.AddOptions<AppSmsOptions>().
                    BindConfiguration("TwilioOptions")
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

            // Old way of setting up options pattern
            //services.ConfigureOptions<AppConfigurationOptionsSetup>();
            //services.ConfigureOptions<AppJwtTokenConfigurationOptionsSetup>();
            //services.ConfigureOptions<AppEmailOptionsSetup>();
            //services.ConfigureOptions<AppSmsOptionsSetup>();
            return services;
        }

        //Contains DI service setup
        private static IServiceCollection AddDIServices(IServiceCollection services)
        {   

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericEFRepository<>), typeof(GenericEFRepository<>));
            services.AddScoped<IGenericDapperRepository, GenericDapperRepository>();
            services.AddScoped<IFakeEmployeeDataGenerator, FakeEmployeeDataGenerator>();
            services.AddScoped<ICustomException, CustomException>();
            services.AddScoped<IImageProcessor, ImageProcessor>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPDFGenerator, PDFGenerator>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISmsService, TwilioSmsService>();
            services.AddHttpClient<ITwilioRestClient, Infrastructure.Services.TwilioSms.TwilioClient>();
            
            return services;
        }
        private static IServiceCollection AddNugetDIServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Application.EmployeeService.Queries.GetAllEmployeeHandler));
            services.AddAutoMapper(typeof(MappingProfiles));
            return services;
        }

        private static IServiceCollection SetupModelValidationError(IServiceCollection services)
        {
            // Setup for Model validation errors to return error 
            // in the way we want
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionConext =>
                {
                    var errors = actionConext.ModelState
                                 .Where(e => e.Value.Errors.Count > 0)
                                 .SelectMany(x => x.Value.Errors)
                                 .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };

            });
            return services;
        }
    }
}
