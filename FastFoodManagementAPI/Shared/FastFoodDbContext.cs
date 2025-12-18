using FastFoodManagementAPI.Features.Orders;
using FastFoodManagementAPI.Features.Products;
using FastFoodManagementAPI.Features.Sales;
using Microsoft.EntityFrameworkCore;

namespace FastFoodManagementAPI.Shared
{
    public class FastFoodDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;

        public FastFoodDbContext(DbContextOptions<FastFoodDbContext> options) : base(options)
        {
        }


        // Fluent API configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product -> OrderItem (1:N)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order -> OrderItem (1:N)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order -> Sale (1:1)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(s => s.Order)
                .HasForeignKey<Payment>(s => s.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Optional: decimal precision
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
               .Property(s => s.TotalAmount)
               .HasColumnType("decimal(18,2)"); // precision 18, scale 2
        }
    }
}
