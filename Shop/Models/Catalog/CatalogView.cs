namespace Shop.Models.Catalog;

public sealed class CatalogView
{
    public IEnumerable<ProductInCatalogView>? Products { get; set; }
    public int Count { get; set; }
}
