namespace Shop.Models.Repository;

public interface IProductRepository : IRepository<Product>
{
    public IEnumerable<Product> Paging<TKey>(int skip, int take, System.Linq.Expressions.Expression<Func<Product, TKey>> order, bool asc = true);
    public IEnumerable<Product> Paging(int skip, int take);
}
