using Microsoft.EntityFrameworkCore;

using Shop.Models.Product;

namespace Shop.Context;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductType> ProductTypes => Set<ProductType>();
    public DbSet<Image> Images => Set<Image>();
    public DbSet<Warranty> Warranties => Set<Warranty>();
    public DbSet<Characteristic> Characteristics => Set<Characteristic>();
    public DbSet<Description> Descriptions => Set<Description>();

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Type)
            .WithMany(p => p.Products)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Characteristic>()
            .HasOne(c => c.Product)
            .WithMany(p => p.Characteristics)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Images)
            .WithOne(i => i.Product)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Warranty>()
            .HasMany(w => w.Products)
            .WithOne(p => p.Warranty)
            .OnDelete(DeleteBehavior.SetNull);

    }
}
