using Application;
using Application.Helpers;
using Application.Interfaces;
using Application.Options;
using Application.Errors;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services = AddDIServices(services);
            services = AddNugetDIServices(services);
            return services;
        }

        //Contains DI service setup
        private static IServiceCollection AddDIServices(IServiceCollection services)
        {
            services.ConfigureOptions<AppConfigurationOptionsSetup>();
            services.ConfigureOptions<AppJwtTokenConfigurationOptionsSetup>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericEFRepository<>), typeof(GenericEFRepository<>));
            services.AddScoped<IGenericDapperRepository, GenericDapperRepository>();
            services.AddScoped<IFakeEmployeeDataGenerator, FakeEmployeeDataGenerator>();
            services.AddScoped<ICustomException, CustomException>();
            services.AddScoped<IImageProcessor, ImageProcessor>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPDFGenerator, PDFGenerator>();
            services.AddScoped<IEmailService, EmailService>();

            // Setup for validation errors to return error 
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
        private static IServiceCollection AddNugetDIServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Application.EmployeeService.Queries.GetAllEmployeeHandler));
            services.AddAutoMapper(typeof(MappingProfiles));
            return services;
        }
    }
}
