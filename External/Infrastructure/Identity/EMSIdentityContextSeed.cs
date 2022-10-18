using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class EMSIdentityContextSeed
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }
                if (!userManager.Users.Any())
                {
                    var user = new IdentityUser
                    {
                        NormalizedUserName = "Root",
                        Email = "subash.barik@gmail.com",
                        UserName = "Root",
                    };

                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
                var createdUser = await userManager.FindByEmailAsync("subash.barik@gmail.com");
                if(createdUser != null)
                {
                    // If User is not assigned with Admin Role then assign it
                    if (!await userManager.IsInRoleAsync(createdUser,"Admin")) 
                    {
                        await userManager.AddToRoleAsync(createdUser, "Admin");
                    }
                    if (!await userManager.IsInRoleAsync(createdUser, "User"))
                    {
                        await userManager.AddToRoleAsync(createdUser, "User");
                    }
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<EMSIdentityContextSeed>();
                logger.LogError(ex.Message);
            }
            

        }
    }
}
