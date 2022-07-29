using Microsoft.EntityFrameworkCore;

using Shop.Constants;
using Shop.Context;
using Shop.Exceptions;
using Shop.Models.Catalog;
using Shop.Models.Product;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationContext _db;

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
    public async Task<Product> GetAsync(int id, bool tracking = false)
    {
        Product product = await GetAsync(id, GetAllInclusions(), tracking);
#warning May be existing
        await CheckingImageExistsAsync(product);

        return product;
    }
    public async Task DeleteAsync(int id)
    {
        Product product = await GetAsync(id, true);

        product.Delete();

        if (product.Description != null)
        {
#warning May be change description delete
            _db.Remove(product.Description);
        }

        _db.Remove(product);
    }
    public async Task SetWarrantyAsync(int productId, int warrantyId, IWarrantyRepository warrantyRepository)
    {
        Warranty warranty = await warrantyRepository.GetAsync(warrantyId, false);

        await SetWarrantyAsync(productId, warranty);
    }
    public async Task SetWarrantyAsync(int productId, Warranty? warranty)
    {
        Product product = await GetAsync(productId, true);

        product.Warranty = warranty;
    }
    public async Task<IQueryable<Product>> SortAndFilterAsync(SortAndFilter model, bool tracking = false)
    {
        IQueryable<Product> query = GetQuery(GetCatalogInclusions(), tracking);
        #region Filters
        if (model.Type != null)
        {
            string? typeName = await _db.ProductTypes
                                        .AsNoTracking()
                                        .Select(t => t.Name)
                                        .FirstOrDefaultAsync(p => p.ToUpper() == model.Type.ToUpper());

            if (typeName == null)
            {
                throw new BadRequestException("Product type is not found");
            }
            query = query.Where(p => p.Type.Name == typeName);
        }

        if (model.PriceFilter != null)
        {
            if (model.PriceFilter.More != null)
            {
                query = query.Where(p => p.Price > model.PriceFilter.More);
            }
            if (model.PriceFilter.Less != null)
            {
                query = query.Where(p => p.Price < model.PriceFilter.Less);
            }
        }

        if (model.Warranty != null)
        {
            query = model.Warranty == true
                ? query.Where(p => p.Warranty != null)
                : query.Where(p => p.Warranty == null);
        }

        if (model.IsStock != null)
        {
            query = model.IsStock == true
                ? query.Where(p => p.IsStock == true)
                : query.Where(p => p.IsStock == false);
        }
        #endregion
        #region Sorts
        if (model.Sort != null)
        {
            System.Linq.Expressions.Expression<Func<Product, double>> expression;

            expression = model.Sort.Type switch
            {
                SortModel.TypeSort.Popularity => p => p.Popularity,
                SortModel.TypeSort.Price => p => (double)p.Price,
                _ => throw new BadRequestException("Sort type is not found")
            };

            query = model.Sort.SortAsc == true
                ? query.OrderBy(expression)
                : query.OrderByDescending(expression);
        }
        #endregion

        return query;
    }
    public async Task AddImagesAsync(int productId, IFormFileCollection uploadedFiles, IImageRepository imageRepository)
    {
        Product product = await GetAsync(productId, GetImageInclusions(), true);

#warning maybe .CreateImagesAsync(..., string -> method) ?
        IEnumerable<Image> images = await imageRepository.CreateImagesAsync(uploadedFiles, $"{PathConst.ProductPath}/{product.Id}");

        product.Images.AddRange(images);
    }
    public async Task DeleteImagesAsync(int productId, IEnumerable<int> imagesId)
    {
        Product product = await GetAsync(productId, GetImageInclusions(), true);

        Dictionary<int, Image> dictionary = new(
            product.Images.Select(i => new KeyValuePair<int, Image>(i.Id, i)));

        // Validation
        foreach (int imageId in imagesId)
        {
            if (imageId < 1)
            {
                throw new ImageIdNegativeException(imageId);
            }

            if (!dictionary.ContainsKey(imageId))
            {
                throw new ProductNotHaveImageException(productId, imageId);
            }
        }

        // Delete all images in array
        foreach (int imageId in imagesId)
        {
            product.DeleteImage(dictionary[imageId]);
        }
    }
    public async Task UpdateAsync(int id, ProductUpdateModel model)
    {
        Product product = await GetAsync(id, _db.Products, true);

        if (model.Name != null)
        {
            product.Name = model.Name;
        }

        if (model.Price != null)
        {
            product.Price = (decimal)model.Price;
        }

        if (model.IsStock != null)
        {
            product.IsStock = (bool)model.IsStock;
        }

        if (model.Description != null)
        {
            product.Description = model.Description.Text == null
                ? null
                : new Description() { Text = model.Description.Text };
        }
    }

    private async Task CheckingImageExistsAsync(params Product[] products)
    {
        bool isChange = false;
        foreach (Product product in products)
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

    // Queries
    private static async Task<Product> GetAsync(int id, IQueryable<Product> queryInclusions, bool tracking = false)
    {
        if (id < 1)
        {
            throw new ProductIdNegativeException(id);
        }
        Product? product = await GetQuery(queryInclusions, tracking).FirstOrDefaultAsync(p => p.Id == id);

        return product ?? throw new ProductNotFoundException(id);
    }
    private static IQueryable<Product> GetQuery(IQueryable<Product> query, bool tracking = false)
    {
        return tracking ? query.AsTracking() : query.AsNoTracking();
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
    private IQueryable<Product> GetImageInclusions()
    {
        return _db.Products
            .Include(p => p.Images);
    }
}