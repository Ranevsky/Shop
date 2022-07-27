using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Shop.Context;
using Shop.Exceptions;
using Shop.Models.Product;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories;

public sealed class WarrantyRepository : IWarrantyRepository
{
    private readonly ApplicationContext _db;
    private readonly IMapper _mapper;
    public WarrantyRepository(ApplicationContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Warranty> GetAsync(int id)
    {
        return await GetAsync(id, _db.Warranties);
    }
    public async Task<WarrantyCountModel> GetCountAsync(int id)
    {
        Warranty warranty = await GetAsync(id, _db.Warranties.Include(w => w.Products), false);
        WarrantyCountModel countModel = _mapper.Map<WarrantyCountModel>(warranty);
        return countModel;
    }
    public async Task UpdateAsync(int id, WarrantyUpdateModel model)
    {
        Warranty warranty = await GetAsync(id, _db.Warranties, true);

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

    private static async Task<Warranty> GetAsync(int id, IQueryable<Warranty> queryInclusions, bool tracking = false)
    {
        if (id < 1)
        {
            throw new WarrantyIdNegativeException(id.ToString());
        }
        Warranty? warranty = await GetQuery(queryInclusions, tracking)
            .FirstOrDefaultAsync(p => p.Id == id);

        return warranty ?? throw new WarrantyNotFoundException(id.ToString());
    }
    private IQueryable<Warranty> GetQuery(bool tracking = false)
    {
        IQueryable<Warranty> query = _db.Warranties;
        return GetQuery(query, tracking);
    }
    private static IQueryable<Warranty> GetQuery(IQueryable<Warranty> query, bool tracking = false)
    {
        return tracking ? query.AsTracking() : query.AsNoTracking();
    }
}
