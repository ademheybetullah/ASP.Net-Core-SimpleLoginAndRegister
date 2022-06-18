using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTask.Identity
{
    public static class SeedAdmin
    {
        public static async Task Seed(UserManager<User> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            var username = configuration["Admin:username"];
            var password = configuration["Admin:password"];
            var email = configuration["Admin:email"];
            var role = configuration["Admin:role"];
            if(await userManager.FindByNameAsync(username) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                var adminUser = new User
                {
                    Name = username,
                    UserName = username,
                    Email = email,
                    EmailConfirmed = true,
                    Surname = "admin",
                    RegistrationDate = DateTime.Now,
                    EmailConfirmDate=DateTime.Now
                };
                var result = await userManager.CreateAsync(adminUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, role);
                }
            }
        }
    }
}
