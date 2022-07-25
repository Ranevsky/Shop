namespace Shop.Exceptions;

public class ProductTypeNotFoundException : NotFoundException
{
    public ProductTypeNotFoundException()
        : base("Product type not found")
    {

    }
    public ProductTypeNotFoundException(string id)
        : base($"Product type with id = '{id}' not found")
    {

    }
}
