namespace Shop.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> FindAsync(int id);
    IEnumerable<T> GetAll();
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entity);
    void Update(T entity);
}