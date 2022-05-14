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
        return db.Products.Include(p => p.Images);
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
}
