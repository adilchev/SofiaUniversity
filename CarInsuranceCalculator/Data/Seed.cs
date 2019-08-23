using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsuranceCalculator.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarInsuranceCalculator.Data
{
    public static class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            //adding customs roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Insurer" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                // creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            // creating a super user who could maintain the web app
            var poweruser = new ApplicationUser
            {
                UserName = Configuration.GetSection("UserSettings")["Username"],
                Email = Configuration.GetSection("UserSettings")["UserEmail"]
            };
            string userPassword = Configuration.GetSection("UserSettings")["UserPassword"];
            var user =  await UserManager.FindByNameAsync(Configuration.GetSection("UserSettings")["Username"]);

            if (user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    // here we assign the new user the "Admin" role 
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
        }
    }
}
