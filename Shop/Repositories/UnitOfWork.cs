using AutoMapper;

using Shop.Context;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _db;
    private readonly IMapper _mapper;

    private IProductRepository? productRepository;
    private IImageRepository? imageRepository;
    private IProductTypeRepository? productTypeRepository;
    private IWarrantyRepository? WarrantyRepository;

    public UnitOfWork(ApplicationContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
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
    public IImageRepository Images
    {
        get
        {
            if (imageRepository == null)
            {
                imageRepository = new ImageRepository(_db);
            }
            return imageRepository;
        }
    }
    public IProductTypeRepository ProductTypes
    {
        get
        {
            if (productTypeRepository == null)
            {
                productTypeRepository = new ProductTypeRepository(_db, _mapper);
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
                WarrantyRepository = new WarrantyRepository(_db, _mapper);
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