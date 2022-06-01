using System.Text.Json.Serialization;

namespace Shop.Models;

public sealed class Warranty
{
    public int Id { get; set; }
    [JsonIgnore]
    public List<Product> Products { get; set; } = new();
    public string Description { get; set; } = null!;
}