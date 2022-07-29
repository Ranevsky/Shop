namespace Shop.Exceptions;

public sealed class ProductNotHaveImageException : NotFoundException
{
    public ProductNotHaveImageException(int productId, int imageId)
        : base($"Product with id = '{productId}' not have image with id = '{imageId}'")
    {

    }
}
