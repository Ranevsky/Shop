using System.Text.Json.Serialization;

namespace Shop.Models;

public sealed class Image
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public Product Product { get; set; } = null!;
    public bool IsExist()
    {
        // File path
        return File.Exists($"{Program.PathToImages}/{Name}");
    }
}