namespace Shop.Exceptions;

public class ProductTypeIdNegativeException : IdNegativeException
{
    public ProductTypeIdNegativeException(string id)
        : base($"Product type with id = '{id}', can not be negative")
    {

    }
}
