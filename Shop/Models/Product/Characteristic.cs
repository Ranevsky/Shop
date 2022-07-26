using System.Text.Json.Serialization;

namespace Shop.Models.Product;

public sealed class Characteristic
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    [JsonIgnore]
    public Product? Product { get; set; }
}
