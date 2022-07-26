namespace Shop.Exceptions;

public sealed class WarrantyIdNegativeException : IdNegativeException
{
    public WarrantyIdNegativeException(string id)
        : base($"Warranty with id = '{id}', can not be negative")
    {

    }
}
