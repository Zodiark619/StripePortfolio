
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StripePortfolio.Areas.GrandArchive.Models;
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
        public DbSet<Card> Card { get; set; }
        public DbSet<Element> Element { get; set; }
        public DbSet<Rarity> Rarity { get; set; }
        public DbSet<CardSet> Set { get; set; }
        public DbSet<Subtype> Subtype { get; set; }
        public DbSet< CardType> CardType { get; set; }
        
        /// ////////////////////////////////////////////////////////
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserInventory> UserInventories { get; set; }
        public DbSet<CardInventory> CardInventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name="Dawn of Ashes Pack",
                    Price=4.99m,
                    YieldContent= "Generate12CardsPack"
                }
               
                );

    //        modelBuilder.Entity<CardInventory>()
    //.HasIndex(ci => new { ci.UserId, ci.CardId })
    //.IsUnique();
            base.OnModelCreating(modelBuilder);

        }
    }
}
