using Domain.Entities.Identity;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace Web
{
    public static class Startup
    {
        //Create EMS database and setup seed data 
        internal static async Task UseEMSDbSetupAsync(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var fakeEmployeeGenerator = services.GetRequiredService<IFakeEmployeeDataGenerator>();
            var hostingEnvironment = services.GetRequiredService<IWebHostEnvironment>();
            try
            {
                var identityContext = services.GetService<EMSIdentityContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await identityContext.Database.MigrateAsync();
                await EMSIdentityContextSeed.SeedAsync(userManager, roleManager, loggerFactory);

                var context = services.GetService<EMSContext>();
                await context.Database.MigrateAsync();
                await EMSContextSeed.SeedAsync(context, loggerFactory, fakeEmployeeGenerator, hostingEnvironment);

                
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occured during migration.");
            }
            
        }
    }
}
