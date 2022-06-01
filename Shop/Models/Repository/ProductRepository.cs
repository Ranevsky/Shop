using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace Shop.Models.Repository;

public sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationContext db) : base(db)
    {

    }

    public new async Task<Product?> FindAsync(int id)
    {
        IQueryable<Product>? products = GetAllInclusions();
        Product? product = await products.FirstOrDefaultAsync(i => i.Id == id);
        if (product != null)
        {
            await CheckingImageExists(product);
        }
        return product;
    }
    public override IEnumerable<Product> GetAll()
    {
        IQueryable<Product>? products = GetAllInclusions();
        return products;
    }
    public async Task<IQueryable<Product>> Paging(SortAndFilter model)
    {
        IQueryable<Product>? productsQury = GetCatalogInclusions();

        #region Filters
        if (model.Type != null)
        {
            if (await db.ProductTypes.FirstOrDefaultAsync(p => p.Name.ToLower() == model.Type.ToLower()) == null)
            {
                throw new Exception("Product type is not found");
            }
            productsQury = productsQury.Where(p => p.Type.Name.ToLower() == model.Type.ToLower());
        }

        if (model.PriceFilter != null)
        {
            if (model.PriceFilter.More != null)
            {
                productsQury = productsQury.Where(p => p.Price > model.PriceFilter.More);
            }
            if (model.PriceFilter.Less != null)
            {
                productsQury = productsQury.Where(p => p.Price < model.PriceFilter.Less);
            }
        }

        if (model.Warranty != null)
        {
            productsQury = model.Warranty == true ?
                productsQury.Where(p => p.Warranty != null) :
                productsQury.Where(p => p.Warranty == null);
        }

        if (model.IsStock != null)
        {
            productsQury = model.IsStock == true ?
                productsQury.Where(p => p.IsStock == true) :
                productsQury.Where(p => p.IsStock == false);
        }
        #endregion

        if (model.Sort != null)
        {
            Expression<Func<Product, double>> expression;

            string sortType = model.Sort.Type.ToLower();
            switch (sortType)
            {
                case "popularity":
                {
                    expression = p => p.Popularity;
                    break;
                }

                case "price":
                {
                    expression = p => (double)p.Price;
                    break;
                }

                default:
                {
                    throw new Exception("Sort type is not found");
                }
            }

            productsQury = model.Sort.Sort_Asc == true ?
                productsQury.OrderBy(expression) :
                productsQury.OrderByDescending(expression);
        }


        return productsQury;
    }

    private IQueryable<Product> GetAllInclusions()
    {
        return db.Products
            .Include(p => p.Description)
            .Include(p => p.Characteristics)
            .Include(p => p.Images)
            .Include(p => p.Type)
            .Include(p => p.Warranty);
    }
    private IQueryable<Product> GetCatalogInclusions()
    {
        return db.Products
            .Include(p => p.Type)
            .Include(p => p.Images);
    }
    private async Task CheckingImageExists(params Product[] products)
    {
        bool isChange = false;
        foreach (Product? product in products)
        {
            if (product.IsNeedDeleteImage())
            {
                isChange = true;
            }
        }
        if (isChange)
        {
            await db.SaveChangesAsync();
        }
    }
}