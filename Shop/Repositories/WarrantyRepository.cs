using Microsoft.EntityFrameworkCore;

using Shop.Context;
using Shop.Exceptions;
using Shop.Models.Product;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories;

public sealed class WarrantyRepository : IWarrantyRepository
{
    private readonly ApplicationContext _db;

    public WarrantyRepository(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<Warranty> GetAsync(int id, bool tracking = false)
    {
        return await GetAsync(id, GetBaseInclusions(), tracking);
    }
    public async Task<WarrantyCountModel> GetCountAsync(int id)
    {
        IdIsNotNegative(id);

        WarrantyCountModel? countModel = await GetQuery(GetProductInclusions(), false)
            .Select(w => new WarrantyCountModel()
            {
                Id = w.Id,
                Name = w.Name,
                Count = w.Products.Count
            })
            .FirstOrDefaultAsync(w => w.Id == id);

        WarrantyIsNotNull(countModel, id.ToString());

        return countModel!;
    }
    public async Task UpdateAsync(int id, WarrantyUpdateModel model)
    {
        Warranty warranty = await GetAsync(id, GetBaseInclusions(), true);

        if (model.Name != null)
        {
            warranty.Name = model.Name;
        }

        if (model.Description != null)
        {
            warranty.Description = model.Description.Text;
        }

    }
    public async Task DeleteAsync(int id)
    {
        Warranty warranty = await GetAsync(id);

        _db.Remove(warranty);
    }
    public async Task AddAsync(Warranty warranty)
    {
        await _db.AddAsync(warranty);
    }
    public IEnumerable<Warranty> GetAll()
    {
        IEnumerable<Warranty> warranties = GetQuery(false);

        return warranties;
    }

    // Queries
    private static async Task<Warranty> GetAsync(int id, IQueryable<Warranty> queryInclusions, bool tracking = false)
    {
        IdIsNotNegative(id);

        Warranty? warranty = await GetQuery(queryInclusions, tracking)
            .FirstOrDefaultAsync(p => p.Id == id);

        WarrantyIsNotNull(warranty, id.ToString());

        return warranty!;
    }
    private IQueryable<Warranty> GetQuery(bool tracking = false)
    {
        IQueryable<Warranty> query = _db.Warranties;

        return GetQuery(query, tracking);
    }
    private static IQueryable<Warranty> GetQuery(IQueryable<Warranty> query, bool tracking = false)
    {
        return tracking
            ? query.AsTracking()
            : query.AsNoTracking();
    }
    private IQueryable<Warranty> GetProductInclusions()
    {
        return _db.Warranties
            .Include(w => w.Products);
    }
    private IQueryable<Warranty> GetBaseInclusions()
    {
        return _db.Warranties;
    }

    // Exceptions
    private static void IdIsNotNegative(int id)
    {
        if (id < 1)
        {
            throw new WarrantyIdNegativeException(id.ToString());
        }
    }
    private static void WarrantyIsNotNull(object? obj, string id)
    {
        if (obj == null)
        {
            throw new WarrantyNotFoundException(id);
        }
    }
}
