namespace Shop.Models.Repository;

public interface IRepository<T> where T : class
{
    T? FindAsync(int id);
    IEnumerable<T> GetAll();
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entity);
    void Update(T entity);
}