namespace Shop.Models.View;

public class ProductCatalogView
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ProductType { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Image { get; set; }
}


public class Catalog
{
    public IEnumerable<ProductCatalogView>? Products { get; set; }
    public long CountProudcts { get; set; }
}