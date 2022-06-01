namespace Shop.Models.View;

public class CatalogView
{
    public IEnumerable<ProductInCatalogView>? Products { get; set; }
    public long CountProudcts { get; set; }
}
