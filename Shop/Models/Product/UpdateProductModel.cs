namespace Shop.Models.Product;

public class UpdateProductModel
{
    public string Name { get; set; } = null!;
    public string Type { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public List<string>? Characteristics { get; set; } = new();
    public string? Warranty { get; set; }
    public bool IsStock { get; set; }
}
