namespace Shop.Exceptions;

public sealed class ImageIdNegativeException : IdNegativeException
{
    public ImageIdNegativeException(string id)
        : base($"Image with id = '{id}', can not be negative")
    {

    }
}
