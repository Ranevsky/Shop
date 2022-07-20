namespace Shop.Exceptions;

public class IdNegativeException : BadRequestException
{
    public IdNegativeException()
        : base("Id can not be negative")
    {

    }
    public IdNegativeException(string? description = null)
        : base(description)
    {

    }
}
