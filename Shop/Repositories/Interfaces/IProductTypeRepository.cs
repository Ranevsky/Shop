using Shop.Models.Product;
using Shop.Exceptions;

namespace Shop.Repositories.Interfaces;

public interface IProductTypeRepository
{
    IEnumerable<ProductType> GetAll();
    bool TryGet(string typeName, out ProductType? type, bool tracking = true);
    /// <exception cref="ProductTypeIsExistException"></exception>
    /// <exception cref="ProductTypeNotFoundException"></exception>
    Task ChangeNameAsync(int id, string newName);
    /// <exception cref="ProductTypeNotFoundException"></exception>
    Task<ProductType> GetAsync(string name, bool tracking = false);
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
    /// <exception cref="ProductTypeNotFoundException"></exception>
    Task<int> GetCountAsync(string name);
    /// <exception cref="ProductTypeIdNegativeException"></exception>
    /// <exception cref="ProductTypeNotFoundException"></exception>
    Task<int> GetCountAsync(int id);

}
