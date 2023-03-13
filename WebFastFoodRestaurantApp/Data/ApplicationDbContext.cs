using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebFastFoodRestaurantApp.Domain;
using WebFastFoodRestaurantApp.Models.Product;

namespace WebFastFoodRestaurantApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<WebFastFoodRestaurantApp.Models.Product.ProductCreateVM> ProductCreateVM { get; set; }
        public DbSet<WebFastFoodRestaurantApp.Models.Product.ProductIndexVM> ProductIndexVM { get; set; }
        public DbSet<WebFastFoodRestaurantApp.Models.Product.ProductEditVM> ProductEditVM { get; set; }

    }
}
