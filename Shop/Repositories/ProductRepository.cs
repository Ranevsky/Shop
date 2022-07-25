using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Shop.Context;
using Shop.Exceptions;
using Shop.Models.Catalog;
using Shop.Models.Product;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private static async Task<Product> FindAsync(int id, IQueryable<Product> queryInclusions)
    {
        if (id < 1)
        {
            throw new ProductIdNegativeException(id.ToString());
        }
        Product? product = await queryInclusions.FirstOrDefaultAsync(p => p.Id == id);

        return product ?? throw new ProductNotFoundException(id.ToString());
    }

    private readonly ApplicationContext _db = null!;
    public ProductRepository(ApplicationContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Product product, IProductTypeRepository productTypeRepository)
    {
        if (productTypeRepository.TryGet(product.Type.Name, out ProductType? typeInDb, true))
        {
            product.Type = typeInDb!;
        }
        else
        {
            await productTypeRepository.AddNotExistAsync(product.Type);
        }
        await _db.Products.AddAsync(product);
    }
    public async Task<Product> FindAsync(int id)
    {
        Product product = await FindAsync(id, GetAllInclusions());

        await CheckingImageExistsAsync(product);

        return product;
    }
    public async Task DeleteAsync(int id)
    {
        Product product = await FindAsync(id);

        product.Delete();
        _db.Remove(product);
    }

    public async Task<IQueryable<Product>> SortAndFilterAsync(SortAndFilter model)
    {
        IQueryable<Product>? productsQury = GetCatalogInclusions();
        #region Filters
        if (model.Type != null)
        {
            ProductType? type = await _db.ProductTypes.FirstOrDefaultAsync(p => p.Name.ToUpper() == model.Type.ToUpper());

            if (type == null)
            {
                throw new BadRequestException("Product type is not found");
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
                    throw new BadRequestException("Sort type is not found");
                }
            }

            productsQury = model.Sort.SortAsc == true
                ? productsQury.OrderBy(expression)
                : productsQury.OrderByDescending(expression);
        }
        #endregion

        return productsQury;
    }
    public async Task AddImagesAsync(int productId, IFormFileCollection uploadedFiles, IImageRepository imageRepository)
    {
        Product product = await FindAsync(productId, _db.Products.Include(p => p.Images));
#warning maybe .CreateImagesAsync(..., string -> method) ?
        IEnumerable<Image> images = await imageRepository.CreateImagesAsync(uploadedFiles, $"{Program.ProductDirectory}/{product.Id}");

        product.Images.AddRange(images);
    }
    public async Task DeleteImagesAsync(int productId, IEnumerable<int> imagesId)
    {
        Product product = await FindAsync(productId, _db.Products.Include(p => p.Images));

        Dictionary<int, Image> dictionary = new(
            product.Images.Select(i => new KeyValuePair<int, Image>(i.Id, i)));

        // Validation
        foreach (int id in imagesId)
        {
            if (id < 1)
            {
                throw new ImageIdNegativeException(id.ToString());
            }

            if (!dictionary.ContainsKey(id))
            {
                throw new ProductNotHaveImageException(productId.ToString(), id.ToString());
            }
        }

        // Delete all images in array
        foreach (int id in imagesId)
        {
            product.DeleteImage(dictionary[id]);
        }
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
}