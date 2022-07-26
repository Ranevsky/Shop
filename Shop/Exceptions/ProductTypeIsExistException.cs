using Shop.Models.Product;

namespace Shop.Exceptions;

public sealed class ProductTypeIsExistException : IsExistException
{
    public ProductTypeIsExistException()
        : base("Product type is exist")
    {

    }

    public ProductTypeIsExistException(string typeName)
        : base($"Product type '{typeName}' is exist")
    {

    }

    public ProductTypeIsExistException(ProductType type)
        : this(type.Name)
    {

    }
}
