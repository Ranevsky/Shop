namespace Shop.Models.Repository;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    Task SaveAsync();

}