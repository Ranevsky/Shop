using Shop.Models.Catalog;
using Shop.Models.Product;

namespace Shop.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IQueryable<Product>> Paging(SortAndFilter model);
}