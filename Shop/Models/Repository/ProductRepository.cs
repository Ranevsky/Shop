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
        var queryProducts = GetAllInclusions();

        if (asc)
        {
            queryProducts = queryProducts.OrderBy(order);
        }
        else
        {
            queryProducts = queryProducts.OrderByDescending(order);
        }

        var products = queryProducts
            .Skip(skip)
            .Take(take)
            .ToArray();

        CheckingImageExists(products);

        return products.AsEnumerable();
    }
    public IEnumerable<Product> Paging(int skip, int take)
    {
        var products = GetAllInclusions()
            .Skip(skip)
            .Take(take)
            .ToArray();

        CheckingImageExists(products);

        return products.AsEnumerable();
    }
    public override Product? Find(int id)
    {
        var products = GetAllInclusions();
        var product = products.FirstOrDefault(i => i.Id == id);
        if (product != null)
        {
            CheckingImageExists(product);
        }
        return product;
    }
    public override IEnumerable<Product> GetAll()
    {
        var products = GetAllInclusions();
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
        foreach (var product in products)
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
    private bool IsNeedDeleteImage(ICollection<Image> images)
    {
        bool isChange = false;
        string filePath;
        List<Image> removeList = new();
        
        foreach(var image in images)
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
        foreach (var item in removeList)
        {
            images.Remove(item);
        }
        return isChange;
    }

    public IQueryable<Product> Page(FilterAndSortModel model)
    {
        // model.Count
        // model.Page

        var productsQury = GetAllInclusions();

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
            if (model.Warranty == true)
            {
                productsQury = productsQury.Where(p => p.Warranty != null);
            }
            else
            {
                productsQury = productsQury.Where(p => p.Warranty == null);
            }
        }

        if (model.IsStock != null)
        {
            if (model.IsStock == true)
            {
                productsQury = productsQury.Where(p => p.IsStock == true);
            }
            else
            {
                productsQury = productsQury.Where(p => p.IsStock == false);
            }
        }
        #endregion

        if (model.Sort != null)
        {
            Expression<Func<Product, double>> expression;

            string sortType = model.Sort.Type.ToLower();
            if (sortType == "popularity")
            {
                expression = p => p.Popularity;
            }
            else if(sortType == "price")
            {
                expression = p => (double) p.Price;
            }
            else
            {
                throw new Exception("Sort type is not found");
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
