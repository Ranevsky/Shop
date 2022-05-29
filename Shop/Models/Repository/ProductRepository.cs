using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Shop.Database;

namespace Shop.Models.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationContext db) : base(db)
    {

    }

    public IEnumerable<Product> Paging<TKey>(int skip, int take, Expression<Func<Product, TKey>> order, bool asc = true)
    {
        IQueryable<Product>? queryProducts = GetAllInclusions();

        queryProducts = asc ? queryProducts.OrderBy(order) : queryProducts.OrderByDescending(order);

        Product[]? products = queryProducts
            .Skip(skip)
            .Take(take)
            .ToArray();

        CheckingImageExists(products);

        return products.AsEnumerable();
    }
    public IEnumerable<Product> Paging(int skip, int take)
    {
        Product[]? products = GetAllInclusions()
            .Skip(skip)
            .Take(take)
            .ToArray();

        CheckingImageExists(products);

        return products.AsEnumerable();
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


    private IQueryable<Product> GetAllInclusions()
    {
        return db.Products
            .Include(p => p.Images)
            .Include(p => p.Type)
            .Include(p => p.Warranty);
    }
    private void CheckingImageExists(params Product[] products)
    {
        bool isChange = false;
        foreach (Product? product in products)
        {
            if (!isChange)
            {
                isChange = IsNeedDeleteImage(product.Images);
                continue;
            }
            IsNeedDeleteImage(product.Images);
        }

        if (isChange)
        {
            db.SaveChanges();
        }
    }
    static private bool IsNeedDeleteImage(ICollection<Image> images)
    {
        bool isChange = false;
        string filePath;
        List<Image> removeList = new();

        foreach (Image? image in images)
        {
            try
            {
                filePath = $"{Program.PathToImages}/{image.Name}";
                if (!File.Exists(filePath))
                {
                    throw new Exception($"'{filePath}' not found");
                }
            }
            catch
            {
                removeList.Add(image);
                isChange = true;
            }
        }
        foreach (Image? item in removeList)
        {
            images.Remove(item);
        }
        return isChange;
    }

    public IQueryable<Product> Page(FilterAndSortModel model)
    {
        IQueryable<Product>? productsQury = GetAllInclusions();

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
}