using Microsoft.EntityFrameworkCore;

using Shop.Context;
using Shop.Exceptions;
using Shop.Extensions;
using Shop.Models.Product;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories;

public class ProductTypeRepository : IProductTypeRepository
{
    private static void NormalFormat(ProductType type)
    {
        type.Name = type.Name.NormalFormat();
    }

    private readonly ApplicationContext _db;

    public ProductTypeRepository(ApplicationContext db)
    {
        _db = db;
    }

    public bool TryGet(string typeName, out ProductType? type, bool tracking = true)
    {
        ProductType? typeInDb = GetQuery(tracking).FirstOrDefault(t => t.Name.ToUpper() == typeName.ToUpper());
        type = typeInDb;

        return typeInDb != null;
    }
    public async Task ChangeNameAsync(int id, string newName)
    {
        if (TryGet(newName, out ProductType? typeInDb, false))
        {
            throw new ProductTypeIsExistException(typeInDb!);
        }

        ProductType type = await GetAsync(id, true);

        type.Name = newName;
        NormalFormat(type);
    }
    public async Task<ProductType> GetAsync(string name, bool tracking = false)
    {
        ProductType? type = await GetQuery(tracking).FirstOrDefaultAsync(t => t.Name == name);
        return type ?? throw new ProductTypeNotFoundException();
    }
    public async Task<ProductType> GetAsync(int id, bool tracking = false)
    {
        if (id < 1)
        {
            throw new ProductTypeIdNegativeException(id.ToString());
        }

        ProductType? type = await GetQuery(tracking).FirstOrDefaultAsync(t => t.Id == id);

        return type ?? throw new ProductTypeNotFoundException(id.ToString());
    }
    public IEnumerable<ProductType> GetAll()
    {
        return GetQuery(false);
    }

    public async Task DeleteAsync(int id, IProductRepository productRepository)
    {
#warning Look in Delete Product Type
        if (id < 1)
        {
            throw new ProductTypeIdNegativeException(id.ToString());
        }

        ProductType? type = await GetQuery(true)
                                    .Include(t => t.Products)
                                    .FirstOrDefaultAsync(t => t.Id == id);
        if (type == null)
        {
            throw new ProductTypeNotFoundException(id.ToString());
        }

        foreach (Product? product in type.Products)
        {
            await productRepository.DeleteAsync(product.Id);
        }
        _db.Remove(type);
    }
    public async Task AddAsync(ProductType type)
    {
        if (TryGet(type.Name, out ProductType? typeInDb, false))
        {
            throw new ProductTypeIsExistException(typeInDb!);
        }

        await AddNotExistAsync(type);
    }
    public async Task AddNotExistAsync(ProductType type)
    {
        NormalFormat(type);

        await _db.AddAsync(type);
    }
    public async Task<int> GetCountAsync(string name)
    {
        ProductType type = await GetAsync(name, true);
        await _db.Entry(type).Collection(t => t.Products).LoadAsync();
        
        return type.Products.Count;
    }
    public async Task<int> GetCountAsync(int id)
    {
        ProductType type = await GetAsync(id, true);
        await _db.Entry(type).Collection(t => t.Products).LoadAsync();

        return type.Products.Count;
    }

    private IQueryable<ProductType> GetQuery(bool tracking = false)
    {
        IQueryable<ProductType> query = _db.ProductTypes;
        if (!tracking)
        {
            query = query.AsNoTracking();
        }
        return query;

    }
}