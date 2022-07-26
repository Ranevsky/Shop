namespace Shop.Exceptions;

public class WarrantyNotFoundException : NotFoundException
{
    public WarrantyNotFoundException()
        : base("Warranty not found")
    {

    }
    public WarrantyNotFoundException(string id)
        : base($"Warranty with id = '{id}' not found")
    {

    }
}
