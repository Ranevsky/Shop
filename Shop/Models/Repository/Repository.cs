namespace Shop.Models.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationContext db;
    public Repository(ApplicationContext db)
    {
        this.db = db;
    }
    public void Add(T entity)
    {
        db.Set<T>().Add(entity);
    }
    public void AddRange(IEnumerable<T> entity)
    {
        db.Set<T>().AddRange(entity);
    }
    public virtual T? Find(int id)
    {
        return db.Set<T>().Find(id);
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