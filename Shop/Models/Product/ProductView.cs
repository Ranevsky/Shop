namespace Shop.Models.Product;

public sealed class ProductView
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ProductType { get; set; } = null!;
    public decimal Price { get; set; }
    public List<string> Images { get; set; } = new();
    public string? Description { get; set; }
    public List<string> Characteristic { get; set; } = new();
    public string? Warranty { get; set; }
    public bool IsStock { get; set; }
}