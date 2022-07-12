namespace Shop.Models.Repository;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext db;
    private IProductRepository productRepository = null!;

    public UnitOfWork(ApplicationContext db)
    {
        this.db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public IProductRepository Products
    {
        get
        {
            if (productRepository == null)
            {
                productRepository = new ProductRepository(db);
            }
            return productRepository;
        }
    }
    public async Task SaveAsync()
    {
        await db.SaveChangesAsync();
    }

    private bool disposed = false;
    private void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                db.Dispose();
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