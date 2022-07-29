namespace Shop.Exceptions;

public sealed class WarrantyNotFoundException : NotFoundException
{
    public WarrantyNotFoundException()
        : base("Warranty not found")
    {

    }
    public WarrantyNotFoundException(int id)
        : base($"Warranty with id = '{id}' not found")
    {

    }
}
