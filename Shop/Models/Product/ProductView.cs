using System.Text.Json.Serialization;

namespace Shop.Models.Product;

public sealed class ProductView
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ProductType { get; set; } = null!;
    public decimal Price { get; set; }
    public List<ImageProductView> Images { get; set; } = new();
    public string? Description { get; set; }
    public List<string> Characteristic { get; set; } = new();
    public ProductWarrantyView? Warranty { get; set; }
    public bool IsStock { get; set; }

    [JsonIgnore]
    public bool IsNeedSave { get; set; }
}