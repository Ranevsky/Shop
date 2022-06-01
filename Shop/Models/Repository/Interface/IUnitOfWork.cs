namespace Shop.Models.Repository;

public interface IUnitOfWork : IDisposable
{
    public IProductRepository Products { get; }
    void Save();

}