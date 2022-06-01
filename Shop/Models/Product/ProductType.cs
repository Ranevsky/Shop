using System.Text.Json.Serialization;

namespace Shop.Models;

public sealed class ProductType
{
    public int Id { get; set; }
    [JsonIgnore]
    public List<Product> Products { get; set; } = new();
    public string Name { get; set; } = null!;
}