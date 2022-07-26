namespace Shop.Models.Product;

public sealed class ProductUpdateModel
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public DescriptionUpdateModel? Description { get; set; }

    // characteristics class
    // public List<string>? Characteristics { get; set; } = new();
    public bool? IsStock { get; set; }
}