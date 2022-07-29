namespace Shop.Exceptions;

public sealed class ProductTypeNotFoundException : NotFoundException
{
    public ProductTypeNotFoundException()
        : base("Product type not found")
    {

    }
    public ProductTypeNotFoundException(int id)
        : base($"Product type with id = '{id}' not found")
    {

    }
}
