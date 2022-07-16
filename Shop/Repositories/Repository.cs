using Shop.Context;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    // db.Set<T>()
    protected readonly ApplicationContext Db;
    public Repository(ApplicationContext db)
    {
        Db = db;
    }
    public async Task AddAsync(T entity)
    {
        await Db.Set<T>().AddAsync(entity);
    }
    public async Task AddRangeAsync(IEnumerable<T> entity)
    {
        await Db.Set<T>().AddRangeAsync(entity);
    }
    public virtual async Task<T?> FindAsync(int id)
    {
        return await Db.Set<T>().FindAsync();
    }
    public virtual IEnumerable<T> GetAll()
    {
        return Db.Set<T>();
    }
    public void Update(T entity)
    {
        Db.Set<T>().Update(entity);
    }
}