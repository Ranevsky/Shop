namespace Shop.Exceptions;

public sealed class WarrantyIdNegativeException : IdNegativeException
{
    public WarrantyIdNegativeException(int id)
        : base($"Warranty with id = '{id}', can not be negative")
    {

    }
}
