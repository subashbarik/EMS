using Domain.Entities.Identity;
using Application.Statics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Identity
{
    public class EMSIdentityContextSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole(Role.Admin));
                    await roleManager.CreateAsync(new IdentityRole(Role.User));
                }
                if (!userManager.Users.Any())
                {
                    var user = new AppUser
                    {
                        NormalizedUserName = "Root",
                        Email = "subash.barik@gmail.com",
                        UserName = "subash.barik@gmail.com",
                        DisplayName = "Root"
                    };

                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
                var createdUser = await userManager.FindByEmailAsync("subash.barik@gmail.com");
                if (createdUser != null)
                {
                    // If User is not assigned with Admin Role then assign it
                    if (!await userManager.IsInRoleAsync(createdUser, Role.Admin))
                    {
                        await userManager.AddToRoleAsync(createdUser, Role.Admin);
                    }
                    if (!await userManager.IsInRoleAsync(createdUser, Role.User))
                    {
                        await userManager.AddToRoleAsync(createdUser, Role.User);
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<EMSIdentityContextSeed>();
                logger.LogError(ex.Message);
            }


        }
    }
}
