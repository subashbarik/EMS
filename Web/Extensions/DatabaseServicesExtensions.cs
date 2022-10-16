using Infrastructure.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Web.Options;

namespace Web.Extensions
{
    public static class DatabaseServicesExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            // Register Database options configuration setup
            services.ConfigureOptions<DatabaseOptionsSetup>();
            services = AddEMSDbContext(services);

            return services;
        }
        //Configuration related to the EMS database
        private static IServiceCollection AddEMSDbContext(IServiceCollection services)
        {
            //TO_DO : Use AddDbContextPool for better performance
            services.AddDbContext<EMSContext>((serviceProvider, option) =>
            {
                // Resolves Database Options , configuring database options this way is
                // very easy and we can change the values in the json configuration file
                // we can just restart the app and it will take effect without the need of
                // re-deployment
                var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;
                
                // additional configuration like below that can be set
                option.UseSqlServer(databaseOptions.ConnectionString, sqlOptionsAction => {
                    sqlOptionsAction.CommandTimeout(databaseOptions.CommandTimeOut);
                });
                option.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                option.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);

            });
            return services;
        }
    }
}
