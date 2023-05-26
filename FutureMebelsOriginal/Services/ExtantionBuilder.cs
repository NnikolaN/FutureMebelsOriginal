﻿using FutureMebelsOriginal.Data;
using Microsoft.AspNetCore.Identity;

namespace ASPShopBag.Services
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDataBase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<MebelsDbContext>();
                var userManager = services.GetRequiredService<UserManager<Customer>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                //Sazdavane na roles
                await SeedRolesAsync(roleManager);
                //sazdavane na SUPER ADMIN s vsi4kite mu roli
                await SeedSuperAdminAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }

            return app;
        }
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            //foreach (var role in Enum.GetValues(Roles))
            //{
            //                    var roleExist = await roleManager.RoleExistsAsync(role); 
            //    if (!roleExist)
            //    { }
            //}

            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));
            await roleManager.CreateAsync(new IdentityRole("Guest"));
        }

        public static async Task SeedSuperAdminAsync(UserManager<Customer> userManager)
        {
            //Seed Default User
            var defaultUser = new Customer
            {
                UserName = "superadmin",
                Email = "mortinka2017@gmail.com",
                FirstMidName = "Nikola Nikolov",
                Adress = "Karlovo",
                PhoneNumber = "0899999999",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                var result = await userManager.CreateAsync(defaultUser, "Niko1234!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, "Admin");
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Guest.ToString());
                    //await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());                    
                }
            }
        }
    }
}