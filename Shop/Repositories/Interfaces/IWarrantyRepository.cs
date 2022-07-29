using Shop.Exceptions;
using Shop.Models.Product;

namespace Shop.Repositories.Interfaces;

public interface IWarrantyRepository
{
    /// <exception cref="WarrantyIdNegativeException"></exception>
    /// <exception cref="WarrantyNotFoundException"></exception>
    Task<Warranty> GetAsync(int id);

    /// <exception cref="WarrantyIdNegativeException"></exception>
    /// <exception cref="WarrantyNotFoundException"></exception>
    Task<WarrantyCountModel> GetCountAsync(int id);

    /// <exception cref="WarrantyIdNegativeException"></exception>
    /// <exception cref="WarrantyNotFoundException"></exception>
    Task UpdateAsync(int id, WarrantyUpdateModel model);

    /// <exception cref="WarrantyIdNegativeException"></exception>
    /// <exception cref="WarrantyNotFoundException"></exception>
    Task DeleteAsync(int id);

    Task AddAsync(Warranty warranty);
    IEnumerable<Warranty> GetAll();
}
