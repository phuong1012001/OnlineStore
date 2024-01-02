using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.Configurations;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.DataAccess.DbContexts
{
    public class OnlineStoreDbContext : DbContext
    {
        public OnlineStoreDbContext()
        {
        }

        public OnlineStoreDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderDetailConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductImageConfiguration());
            builder.ApplyConfiguration(new StockConfiguration());
            builder.ApplyConfiguration(new StockEventConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CartConfiguration());
            builder.ApplyConfiguration(new CartDetailConfiguration());
        }

        public DbSet<Category> Categorys { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> productImages { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StockEvent> StockEvents { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartDetail> CartDetails { get; set; }
    }
}
