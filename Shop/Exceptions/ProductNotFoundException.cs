﻿namespace Shop.Exceptions;

public sealed class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException()
        : base("Product not found")
    {

    }
    public ProductNotFoundException(int id)
        : base($"Product with id = '{id}' not found")
    {

    }
}