namespace Shop.Models.Catalog;

public sealed class ProductInCatalogView
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ProductType { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Image { get; set; }
}