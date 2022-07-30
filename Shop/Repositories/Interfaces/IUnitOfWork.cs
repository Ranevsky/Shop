namespace Shop.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IProductTypeRepository ProductTypes { get; }
    IWarrantyRepository Warranties { get; }
    Task SaveAsync();

}