namespace Shop.Models;

public class Warranty
{
    public int Id { get; set; }
    public List<Product> Products { get; set; } = new();
    public string Description { get; set; } = null!;
}
