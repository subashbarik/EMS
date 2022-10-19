using Application;
using Application.Helpers;
using Application.Interfaces;
using Application.Options;
using Domain.Errors;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using MediatR;

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
            services.ConfigureOptions<AppTokenConfigurationOptionsSetup>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericEFRepository<>), typeof(GenericEFRepository<>));
            services.AddScoped<IGenericDapperRepository, GenericDapperRepository>();
            services.AddScoped<IFakeEmployeeDataGenerator, FakeEmployeeDataGenerator>();
            services.AddScoped<ICustomException, CustomException>();
            services.AddScoped<IImageProcessor, ImageProcessor>();
            services.AddScoped<ITokenService, TokenService>();
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
