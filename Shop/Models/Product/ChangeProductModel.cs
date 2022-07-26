namespace Shop.Models.Product;

public sealed class ChangeProductModel
{
    public string? Name { get; set; }
    public ProductType? Type { get; set; }
    public decimal? Price { get; set; }
    public bool? IsStock { get; set; }
    //public Description? Description { get; set; }
    //public List<Characteristic>? Characteristics { get; set; } = new();
    //public Warranty? Warranty { get; set; }
}