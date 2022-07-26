using System.Text.Json.Serialization;

namespace Shop.Models.Product;

public sealed class Image : IDelete
{
    public int Id { get; set; }
    /// <summary>
    /// Example: foo.png
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// Example: products/2
    /// </summary>
#warning create class
    public string Path { get; set; } = null!;

    [JsonIgnore]
    public Product? Product { get; set; }
#warning Maybe not exist, get url (if IsExist)
    [JsonIgnore]
    private string FullPath => $"{Program.PathToImages}{Path}/{Name}";
    [JsonIgnore]
    public bool IsExists => File.Exists(FullPath);
    public void Delete()
    {
        if (!IsExists)
        {
            return;
        }

#warning Add logger
        File.Delete(FullPath);
        Console.WriteLine($"Delete image '{Path}/{Name}'");
    }
}