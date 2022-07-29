namespace Shop.Exceptions;

public sealed class ProductTypeIdNegativeException : IdNegativeException
{
    public ProductTypeIdNegativeException(int id)
        : base($"Product type with id = '{id}', can not be negative")
    {

    }
}
