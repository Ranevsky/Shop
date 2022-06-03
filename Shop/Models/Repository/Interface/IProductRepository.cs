namespace Shop.Models.Repository;

public interface IProductRepository : IRepository<Product>
{
    Task<IQueryable<Product>> Paging(SortAndFilter model);
}