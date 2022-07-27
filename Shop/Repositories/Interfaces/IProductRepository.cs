using Shop.Exceptions;
using Shop.Models.Catalog;
using Shop.Models.Product;

namespace Shop.Repositories.Interfaces;

public interface IProductRepository
{
    /// <exception cref="ProductNotFoundException"></exception>
    /// <exception cref="ProductIdNegativeException"></exception>
    Task<Product> GetAsync(int id, bool tracking = false);

    Task AddAsync(Product product, IProductTypeRepository productTypeRepository);

    /// <exception cref="ProductNotFoundException"></exception>
    /// <exception cref="ProductIdNegativeException"></exception>
    Task DeleteAsync(int productId);

    /// <exception cref="BadRequestException"></exception>
    Task<IQueryable<Product>> SortAndFilterAsync(SortAndFilter model);

    /// <exception cref="ProductNotFoundException"></exception>
    /// <exception cref="ProductIdNegativeException"></exception>
    /// <exception cref="UploadFileIsEmptyException"></exception>
    /// <exception cref="FileIsNotImageException"></exception>
    Task AddImagesAsync(int productId, IFormFileCollection uploadedFiles, IImageRepository imageRepository);

    /// <exception cref="ProductNotFoundException"></exception>
    /// <exception cref="ProductIdNegativeException"></exception>
    /// <exception cref="ProductNotHaveImageException"></exception>
    Task DeleteImagesAsync(int productId, IEnumerable<int> imagesId);

    /// <exception cref="ProductNotFoundException"></exception>
    /// <exception cref="ProductIdNegativeException"></exception>
    Task UpdateAsync(int id, ProductUpdateModel model);

    /// <exception cref="ProductNotFoundException"></exception>
    /// <exception cref="ProductIdNegativeException"></exception>
    /// <exception cref="WarrantyIdNegativeException"></exception>
    /// <exception cref="WarrantyNotFoundException"></exception>
    Task SetWarrantyAsync(int productId, int warrantyId, IWarrantyRepository warrantyRepository);

    /// <exception cref="ProductNotFoundException"></exception>
    /// <exception cref="ProductIdNegativeException"></exception>
    Task SetWarrantyAsync(int productId, Warranty? warranty);
}