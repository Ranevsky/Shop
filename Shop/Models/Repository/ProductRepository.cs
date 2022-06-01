using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace Shop.Models.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationContext db) : base(db)
    {

    }

    public override Product? Find(int id)
    {
        IQueryable<Product>? products = GetAllInclusions();
        Product? product = products.FirstOrDefault(i => i.Id == id);
        if (product != null)
        {
            CheckingImageExists(product);
        }
        return product;
    }
    public override IEnumerable<Product> GetAll()
    {
        IQueryable<Product>? products = GetAllInclusions();
        return products;
    }
    public IQueryable<Product> Paging(SortAndFilter model)
    {
        IQueryable<Product>? productsQury = GetCatalogInclusions();

        #region Filters
        if (model.Type != null)
        {
            if (db.ProductTypes.FirstOrDefault(p => p.Name.ToLower() == model.Type.ToLower()) == null)
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

            if (model.Sort.Sort_Asc == true)
            {
                productsQury = productsQury.OrderBy(expression);
                Console.WriteLine("Sorting by asc");
            }
            else
            {
                productsQury = productsQury.OrderByDescending(expression);
                Console.WriteLine("Sorting by desc");
            }
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
    private void CheckingImageExists(params Product[] products)
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
            db.SaveChanges();
        }
    }
}