namespace Shop.Models;


public class ProductType
{
    public int Id { get; set; }
    public List<Product> Products { get; set; } = new();
    public string Name { get; set; } = null!;
}
