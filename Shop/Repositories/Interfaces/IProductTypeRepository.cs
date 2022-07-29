using Shop.Exceptions;
using Shop.Models.Product;

namespace Shop.Repositories.Interfaces;

public interface IProductTypeRepository
{
    IEnumerable<ProductType> GetAll();

    bool TryGet(string name, out ProductType? type, bool tracking = true);

    /// <exception cref="ProductTypeIsExistException"></exception>
    /// <exception cref="ProductTypeNotFoundException"></exception>
    Task ChangeNameAsync(int id, string newName);

    /// <exception cref="ProductTypeIdNegativeException"></exception>
    /// <exception cref="ProductTypeNotFoundException"></exception>
    Task<ProductType> GetAsync(int id, bool tracking = false);

    /// <exception cref="ProductTypeIdNegativeException"></exception>
    /// <exception cref="ProductTypeNotFoundException"></exception>
    Task DeleteAsync(int id, IProductRepository productRepository);

    /// <exception cref="ProductTypeIsExistException"></exception>
    Task AddAsync(ProductType type);

    /// <summary>
    /// Don't use, if you are not sure of existing
    /// </summary>
    Task AddNotExistAsync(ProductType type);

    /// <exception cref="ProductTypeIdNegativeException"></exception>
    /// <exception cref="ProductTypeNotFoundException"></exception>
    Task<ProductTypeCountModel> GetCountAsync(int id);
}
