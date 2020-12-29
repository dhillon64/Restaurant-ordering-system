using Microsoft.AspNetCore.Identity;
using Restaurant_ordering_system.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_ordering_system
{
    public static class SeedData
    {
        public static void Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("Admin").Result == null)
            {
                var user = new ApplicationUser
                {
                    Name = "Admin",
                    UserName= "admin@localhost.com",
                    Email = "admin@localhost.com"
                };

                var result = userManager.CreateAsync(user, "Password1!").Result;

                if (result.Succeeded) 
                {
                    userManager.AddToRoleAsync(user,"Manager").Wait();
                }

            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Manager").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Manager"
                };

                var result=roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Customer").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Customer"
                };

                var result = roleManager.CreateAsync(role).Result;
            }
        }

    }
}
