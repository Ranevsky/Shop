namespace Shop.Exceptions;

public class ProductNotHaveImageException : NotFoundException
{
    public ProductNotHaveImageException(string productId, string imageId)
        : base($"Product with id = '{productId}' not have image with id = '{imageId}'")
    {

    }
}
