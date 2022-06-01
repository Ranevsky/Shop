namespace Shop.Models.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    // db.Set<T>()
    protected readonly ApplicationContext db;
    public Repository(ApplicationContext db)
    {
        this.db = db;
    }
    public async Task AddAsync(T entity)
    {
        await db.Set<T>().AddAsync(entity);
    }
    public async Task AddRangeAsync(IEnumerable<T> entity)
    {
        await db.Set<T>().AddRangeAsync(entity);
    }
    public T? FindAsync(int id)
    {
        return db.Set<T>().FindAsync().Result;
    }
    public virtual IEnumerable<T> GetAll()
    {
        return db.Set<T>();
    }
    public void Update(T entity)
    {
        db.Set<T>().Update(entity);
    }
}