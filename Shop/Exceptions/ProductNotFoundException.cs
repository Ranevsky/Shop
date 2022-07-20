namespace Shop.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException()
        : base("Product not found")
    {

    }
    public ProductNotFoundException(string id)
        : base($"Product with id = '{id}' not found")
    {

    }
}