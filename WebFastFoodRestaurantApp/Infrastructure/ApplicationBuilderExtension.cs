using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFastFoodRestaurantApp.Data;
using WebFastFoodRestaurantApp.Domain;

namespace WebFastFoodRestaurantApp.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);
            


            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataCategory);

            var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedBrands(dataBrand);
            return app;
        }
        

        public static void SeedBrands(ApplicationDbContext dataBrand)
        {
            if (dataBrand.Brands.Any())
            {
                return;
            }
            dataBrand.Brands.AddRange(new[]
            {
                new Brand{BrandName="Вегетарианско"},
                new Brand{BrandName="Месно"},
                new Brand{BrandName="Здравословни"},
                new Brand{BrandName="Детско"}

            });
            dataBrand.SaveChanges();
        }
        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if (dataCategory.Categories.Any())
            {
                return;
            }
            dataCategory.Categories.AddRange(new[]
            {
                new Category{CategoryName="Салати"},
                new Category{CategoryName="Скара"},
                new Category{CategoryName="Десерти"},
                new Category{CategoryName="Напитки"}

            });
            dataCategory.SaveChanges();
        }


        private static async Task RoleSeeder(IServiceProvider serviceProder)
            {
                var roleManager = serviceProder.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roleNames = { "Administrator", "Client" };

                IdentityResult roleResult;

                foreach (var role in roleNames)
                {
                    var roleExit = await roleManager.RoleExistsAsync(role);

                    if (!roleExit)
                    {
                        roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            private static async Task SeedAdministrator(IServiceProvider serviceProvider)
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                if (await userManager.FindByNameAsync("admin") == null)
                {
                    ApplicationUser user = new ApplicationUser();
                    user.FirstName = "admin";
                    user.LastName = "admin";
                    user.PhoneNumber = "0888888";
                    user.UserName = "admin";
                    user.Email = "admin@admin.com";

                    var result = await userManager.CreateAsync
                        (user, "Admin123456");

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Administrator").Wait();
                    }
                }
            }
        }
    }

