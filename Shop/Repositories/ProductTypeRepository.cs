using System.Text;

using Microsoft.EntityFrameworkCore;

using Shop.Context;
using Shop.Exceptions;
using Shop.Models.Product;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories;

public sealed class ProductTypeRepository : IProductTypeRepository
{
    private readonly ApplicationContext _db;

    public ProductTypeRepository(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<ProductType> GetAsync(int id, bool tracking = false)
    {
        IdIsNotNegative(id);

        ProductType? type = await GetQuery(tracking)
                                    .FirstOrDefaultAsync(t => t.Id == id);

        ProductTypeIsNotNull(type, id);

        return type!;
    }
    public bool TryGet(string typeName, out ProductType? type, bool tracking = true)
    {
        ProductType? typeInDb = GetQuery(tracking)
                                .FirstOrDefault(t => t.Name.ToUpper() == typeName.ToUpper());

        type = typeInDb;

        return type != null;
    }
    public IEnumerable<ProductType> GetAll()
    {
        return GetQuery(false);
    }
    public async Task AddAsync(ProductType type)
    {
        if (TryGet(type.Name, out ProductType? typeInDb, false))
        {
            ProductTypeIsNull(typeInDb!);
        }

        await AddNotExistAsync(type);
    }
    public async Task AddNotExistAsync(ProductType type)
    {
        NormalFormat(type);

        await _db.AddAsync(type);
    }
    public async Task DeleteAsync(int id, IProductRepository productRepository)
    {
#warning Look in Delete Product Type
        IdIsNotNegative(id);

        ProductType? type = await GetQuery(GetProductInclusions(), true)
                                    .FirstOrDefaultAsync(t => t.Id == id);

        ProductTypeIsNotNull(type, id);

        foreach (Product? product in type!.Products)
        {
            await productRepository.DeleteAsync(product.Id);
        }
        _db.Remove(type);
    }
    public async Task ChangeNameAsync(int id, string newName)
    {
        if (TryGet(newName, out ProductType? typeInDb, false))
        {
            ProductTypeIsNull(typeInDb!);
        }

        ProductType type = await GetAsync(id, true);

        type.Name = newName;
        NormalFormat(type);
    }
    public async Task<ProductTypeCountModel> GetCountAsync(int id)
    {
        IdIsNotNegative(id);

        ProductTypeCountModel? countModel = await GetQuery(GetProductInclusions(), false)
            .Select(t => new ProductTypeCountModel()
            {
                Id = t.Id,
                Name = t.Name,
                Count = t.Products.Count
            })
            .FirstOrDefaultAsync(t => t.Id == id);

        ProductTypeIsNotNull(countModel, id);

        return countModel!;
    }

    private static void NormalFormat(ProductType type)
    {
        StringBuilder sb = new(type.Name.ToLower());
        sb[0] = char.ToUpper(sb[0]);

        type.Name = sb.ToString();
    }

    // Queries
    private static IQueryable<ProductType> GetQuery(IQueryable<ProductType> query, bool tracking = false)
    {
        return tracking
            ? query.AsTracking()
            : query.AsNoTracking();
    }
    private IQueryable<ProductType> GetQuery(bool tracking = false)
    {
        IQueryable<ProductType> query = _db.ProductTypes;

        return GetQuery(query, tracking);
    }
    private IQueryable<ProductType> GetProductInclusions()
    {
        IQueryable<ProductType> query = _db.ProductTypes
                                            .Include(t => t.Products);

        return query;
    }

    // Exceptions
    private static void IdIsNotNegative(int id)
    {
        if (id < 1)
        {
            throw new ProductTypeIdNegativeException(id);
        }
    }
    private static void ProductTypeIsNotNull(object? obj, int id)
    {
        if (obj == null)
        {
            throw new ProductTypeNotFoundException(id);
        }
    }
    private static void ProductTypeIsNull(ProductType type)
    {
        throw new ProductTypeIsExistException(type);
    }
}