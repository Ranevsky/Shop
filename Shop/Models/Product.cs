using System.ComponentModel.DataAnnotations;

namespace Shop.Models;

public class Product
{
    [Key]
    public int Id { get; set; } = default;
    public string Name { get; set; } = null!;
    public string ProductType { get; set; } = null!;
    public decimal Price { get; set; } = default;
    public List<Image> Images { get; set; } = new();
    public int Popularity { get; set; } = default;
    public string? Description { get; set; } = default;
    public List<string> Characteristic { get; set; } = new();
    public string? Warranty { get; set; } = default;
}

