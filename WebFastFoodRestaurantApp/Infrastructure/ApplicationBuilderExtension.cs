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

            var dataTypeFood = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedTypeFoods(dataTypeFood);
            return app;
        }
        

        public static void SeedTypeFoods(ApplicationDbContext dataTypeFood)
        {
            if (dataTypeFood.TypeFoods.Any())
            {
                return;
            }
            dataTypeFood.TypeFoods.AddRange(new[]
            {
                new TypeFood{TypeFoodName="Vegeterians"},
                new TypeFood{TypeFoodName="Meat"},
                new TypeFood{TypeFoodName="Healthy"},
                new TypeFood{TypeFoodName="Childish"},
                new TypeFood{TypeFoodName="Alcohol"},
                new TypeFood{TypeFoodName="Alcohol-free"}

            });
            dataTypeFood.SaveChanges();
        }
        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if (dataCategory.Categories.Any())
            {
                return;
            }
            dataCategory.Categories.AddRange(new[]
            {
                new Category{CategoryName="Salads"},
                new Category{CategoryName="Grilled"},
                new Category{CategoryName="Desserts"},
                new Category{CategoryName="Drinks"}

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

