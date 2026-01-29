using Microsoft.EntityFrameworkCore;
using Order.Models;

namespace Order.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<Models.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Order configuration
        modelBuilder.Entity<Models.Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.UserId).IsRequired().HasMaxLength(256);
            entity.Property(o => o.TotalAmount).HasPrecision(18, 2);
            entity.Property(o => o.ShippingAddress).HasMaxLength(500);
            entity.Property(o => o.ShippingCity).HasMaxLength(100);
            entity.Property(o => o.ShippingZipCode).HasMaxLength(20);
            entity.Property(o => o.ShippingCountry).HasMaxLength(100);
            entity.Property(o => o.CreatedAt).IsRequired();

            // One-to-many relationship
            entity.HasMany(o => o.Items)
                  .WithOne(i => i.Order)
                  .HasForeignKey(i => i.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // OrderItem configuration
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.ProductId).IsRequired().HasMaxLength(256);
            entity.Property(i => i.ProductName).IsRequired().HasMaxLength(256);
            entity.Property(i => i.Quantity).IsRequired();
            entity.Property(i => i.UnitPrice).HasPrecision(18, 2);
            entity.Ignore(i => i.TotalPrice); // Computed property
        });
    }
}
