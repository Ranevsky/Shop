using Shop.Context;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _db;

    private IProductRepository? productRepository;
    private IProductTypeRepository? productTypeRepository;
    private IWarrantyRepository? WarrantyRepository;

    public UnitOfWork(ApplicationContext db)
    {
        _db = db;
    }

    public IProductRepository Products
    {
        get
        {
            if (productRepository == null)
            {
                productRepository = new ProductRepository(_db);
            }
            return productRepository;
        }
    }

    public IProductTypeRepository ProductTypes
    {
        get
        {
            if (productTypeRepository == null)
            {
                productTypeRepository = new ProductTypeRepository(_db);
                ;
            }
            return productTypeRepository;
        }
    }

    public IWarrantyRepository Warranties
    {
        get
        {
            if (WarrantyRepository == null)
            {
                WarrantyRepository = new WarrantyRepository(_db);
            }
            return WarrantyRepository;
        }
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    private bool disposed = false;
    private void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}