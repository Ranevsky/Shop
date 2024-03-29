﻿namespace Shop.Models.Product;

public sealed class ProductAddModel
{
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public List<string>? Characteristics { get; set; } = new();
    public bool IsStock { get; set; }
}
