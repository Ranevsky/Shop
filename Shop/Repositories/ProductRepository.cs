using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Shop.Context;
using Shop.Models.Catalog;
using Shop.Models.Product;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationContext _db = null!;
    public ProductRepository(ApplicationContext db)
    {
        _db = db;
    }

    public async Task<Product?> FindAsync(int id)
    {
#warning Watching product != null exception system
        IQueryable<Product>? products = GetAllInclusions();
        Product? product = await products.FirstOrDefaultAsync(i => i.Id == id);
        if (product != null)
        {
            await CheckingImageExistsAsync(product);
        }
        return product;
    }
    public IEnumerable<Product> GetAll()
    {
        IQueryable<Product>? products = GetAllInclusions();
        return products;
    }
    public async Task<IQueryable<Product>> PagingAsync(SortAndFilter model)
    {
        IQueryable<Product>? productsQury = GetCatalogInclusions();
        #region Filters
        if (model.Type != null)
        {
            ProductType? type = await _db.ProductTypes.FirstOrDefaultAsync(p => p.Name.ToUpper() == model.Type.ToUpper());

            if (type == null)
            {
                throw new Exception("Product type is not found");
            }
            productsQury = productsQury.Where(p => p.Type.Name == type.Name);
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
        #region Sorts
        if (model.Sort != null)
        {
            Expression<Func<Product, double>> expression;

            switch (model.Sort.Type)
            {
                case SortModel.TypeSort.Popularity:
                {
                    expression = p => p.Popularity;
                    break;
                }

                case SortModel.TypeSort.Price:
                {
                    expression = p => (double)p.Price;
                    break;
                }

                default:
                {
                    throw new Exception("Sort type is not found");
                }
            }

            productsQury = model.Sort.SortAsc == true
                ? productsQury.OrderBy(expression)
                : productsQury.OrderByDescending(expression);
        }
        #endregion

        return productsQury;
    }

    private IQueryable<Product> GetAllInclusions()
    {
        return _db.Products
            //.AsSplitQuery() // If there is a very large duplicate database
            .Include(p => p.Description)
            .Include(p => p.Characteristics)
            .Include(p => p.Images)
            .Include(p => p.Type)
            .Include(p => p.Warranty);
    }
    private IQueryable<Product> GetCatalogInclusions()
    {
        return _db.Products
            .Include(p => p.Type)
            .Include(p => p.Images.Take(1));
    }
    private async Task CheckingImageExistsAsync(params Product[] products)
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
            await _db.SaveChangesAsync();
        }
    }


    public async Task DeleteAsync(int id)
    {
        Product? product = await FindAsync(id);

        if (product == null)
        {
            return;
        }

        product.Delete();
        _db.Remove(product);
        await _db.SaveChangesAsync();
    }


    public async Task AddImagesAsync(int id, IFormFileCollection uploadedFiles, IImageRepository imageRepository)
    {
        Product? product = await _db.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            throw new Exception("Product not found");
        }

        IEnumerable<Image> images = await imageRepository.CreateImagesAsync(uploadedFiles, $"{Program.ProductDirectory}/{product.Id}");

        product.Images.AddRange(images);
        await _db.SaveChangesAsync();
    }
    public async Task DeleteImagesAsync(int productId, params int[] imagesId)
    {
        Product? product = await _db.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
        {
            throw new Exception("Product not found");
        }

        Dictionary<int, Image> dictionary = new(
            product.Images.Select(i => new KeyValuePair<int, Image>(i.Id, i)));

        if (!Checking(dictionary, imagesId))
        {
            throw new Exception("Not exist");
        }

        Delete(product, imagesId);

        await _db.SaveChangesAsync();

        bool Checking(Dictionary<int, Image> dictionary, params int[] imagesId)
        {
            foreach (int id in imagesId)
            {
                if (!dictionary.ContainsKey(id))
                {
                    return false;
                }
            }
            return true;
        }
        void Delete(Product product, params int[] imagesId)
        {
            foreach (int id in imagesId)
            {
                product.DeleteImage(dictionary[id]);
            }
        }
    }

    public async Task AddAsync(Product product)
    {
        await _db.Products.AddAsync(product);
    }
}

