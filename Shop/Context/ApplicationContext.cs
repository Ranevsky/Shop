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
            .HasMany(p => p.Images)
            .WithOne(p => p.Product)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
