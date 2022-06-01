namespace Shop.Models.Repository;

public interface IRepository<T> where T : class
{
    T? Find(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void AddRange(IEnumerable<T> entity);
    void Update(T entity);
}