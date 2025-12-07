
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StripePortfolio.Models;

namespace StripePortfolio.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserInventory> UserInventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name="Leaf",
                    Price=13.99m
                },
                new Product
                {
                    Id = 2,
                    Name="Table",
                    Price=145.99m
                },
                new Product
                {
                    Id = 3,
                    Name="Chair",
                    Price=27.12m
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
