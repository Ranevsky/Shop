namespace Shop.Exceptions;

public class IsExistException : BadRequestException
{
    public IsExistException(string description)
        : base(description)
    {

    }
}
