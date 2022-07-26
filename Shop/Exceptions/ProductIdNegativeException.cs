namespace Shop.Exceptions;

public sealed class ProductIdNegativeException : IdNegativeException
{
    public ProductIdNegativeException(string id)
        : base($"Product with id = '{id}', can not be negative")
    {

    }
}
