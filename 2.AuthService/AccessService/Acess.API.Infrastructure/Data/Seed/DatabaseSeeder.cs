using Access.API.Domain.Entities;
using Access.API.Infrastructure.Data.Seed.Models;
using Access.API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Access.API.Infrastructure.Data.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(
            AuthDbContext context,
            UserManager<AuthUser> userManager,
            RoleManager<IdentityRole<long>> roleManager, IOptions<SeederSettings> seederOptions)
        {
            var settings = seederOptions.Value;
        
            //  Seed Roles from config
            foreach (var role in settings.DefaultRoles.Distinct())
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<long>(role));
                }
            }
            //  Seed Users from config
            foreach (var user in settings.DefaultUsers)
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email);
                if (existingUser == null)
                {
                    var newUser = new AuthUser
                    {
                        UserName = user.Email,
                        Email = user.Email,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(newUser, user.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newUser, user.Role);
                    }
                }
            }

        }
    }
}
