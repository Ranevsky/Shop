﻿namespace Shop.Models.View;

public sealed class CatalogView
{
    public IEnumerable<ProductInCatalogView>? Products { get; set; }
    public long CountProducts { get; set; }
}