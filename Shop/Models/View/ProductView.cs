namespace Shop.Models.View;

public class ProductView
{
    public int Id { get; set; } = default;
    public string Name { get; set; } = null!;
    public string ProductType { get; set; } = null!;
    public decimal Price { get; set; } = default;
    public List<ImageView> Images { get; set; } = new();
}
