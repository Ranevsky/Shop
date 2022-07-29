namespace Shop.Models.Product;

/// <summary>
/// <b>Warning:</b> Not supported AutoMapper
/// </summary>
public sealed class ProductTypeCountModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Count { get; set; }
}
