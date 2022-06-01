namespace Shop.Models.Repository;

public interface IProductRepository : IRepository<Product>
{
    public IQueryable<Product> Paging(SortAndFilter model);
}