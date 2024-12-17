using Ecommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace Ecommerce.Data
{
    public class ApplicationDbcontext:IdentityDbContext<AppUser>
    {
   
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> Orderdetails { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<AppUser> appUsers { get; set; }
        public DbSet<CartProduct> cartProducts { get; set; }
        public DbSet<Category> categories { get; set; }

        public ApplicationDbcontext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var passwordHasher = new PasswordHasher<AppUser>();

            // Seeding Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Home Appliances" }
            );

            // Seeding Products with their respective categories
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Smartphone", Price = 499.99m, Description = "A high-end smartphone.", CategoryId = 1 },
                new Product { Id = 2, Name = "Laptop", Price = 799.99m, Description = "A powerful laptop.", CategoryId = 1 },
                new Product { Id = 3, Name = "T-shirt", Price = 19.99m, Description = "A comfortable cotton t-shirt.", CategoryId = 2 },
                new Product { Id = 4, Name = "Jeans", Price = 39.99m, Description = "A pair of stylish jeans.", CategoryId = 2 },
                new Product { Id = 5, Name = "Washing Machine", Price = 299.99m, Description = "A powerful washing machine.", CategoryId = 3 },
                new Product { Id = 6, Name = "Microwave Oven", Price = 129.99m, Description = "A compact microwave oven.", CategoryId = 3 }
            );

            var user1 = new AppUser
            {
                Id = "1", // Generate a unique ID
                UserName = "Aalaa",
                NormalizedUserName = "AALAA",
                CartId = null, // Explicitly set CartId to null to avoid linking
                Cart = null  ,  // Ensure navigation property is not tracked
                pass="123",
                PasswordHash = passwordHasher.HashPassword(null, "123"),
                role ="admin"
               
            };

            var user2 = new AppUser
            {
                Id = "2",
                UserName = "JohnDoe",
                NormalizedUserName = "JOHNDOE",
                CartId = null, // Explicitly set CartId to null
                Cart = null ,   // Ensure navigation property is not tracked
                pass="123456",
                PasswordHash=passwordHasher.HashPassword(null, "123456"),
                role = "ordinary"
            };

            // Seed AppUsers
            modelBuilder.Entity<AppUser>().HasData(user1, user2);


        }



    }
}
    

