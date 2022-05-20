using System.ComponentModel.DataAnnotations;

namespace Shop.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ProductType Type { get; set; } = null!;
    public decimal Price { get; set; }
    public List<Image> Images { get; set; } = new();
    public int Popularity { get; set; }
    public string? Description { get; set; }
    public List<string> Characteristic { get; set; } = new();
    public Warranty? Warranty { get; set; }
    public bool IsStock { get; set; }
}

