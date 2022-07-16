namespace Shop.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IImageRepository Images { get; }
    Task SaveAsync();

}