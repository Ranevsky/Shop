using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Models;

namespace Shop.Database;

public class ApplicationContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Image> Images => Set<Image>();
    public DbSet<Warranty> Warranties => Set<Warranty>();
    public DbSet<ProductType> ProductTypes => Set<ProductType>();
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var splitStringConverter = new ValueConverter<List<string>, string>(
            v => string.Join(";", v), 
            v => v.Split(new[] { ';' }).ToList()); 

        modelBuilder.Entity<Product>().Property(nameof(Product.Characteristic)).HasConversion(splitStringConverter);
    }
}
