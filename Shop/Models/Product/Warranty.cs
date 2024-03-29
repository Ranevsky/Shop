﻿using System.Text.Json.Serialization;

namespace Shop.Models.Product;

public sealed class Warranty
{
    public int Id { get; set; }
    [JsonIgnore]
    public List<Product> Products { get; set; } = new();
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}