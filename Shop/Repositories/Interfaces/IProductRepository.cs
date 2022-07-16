using Shop.Models.Catalog;
using Shop.Models.Product;

namespace Shop.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IQueryable<Product>> PagingAsync(SortAndFilter model);
    Task AddImagesAsync(int id, IFormFileCollection uploadedFiles, IImageRepository imageRepository);
    Task DeleteImagesAsync(int productId, params int[] imagesId);
    Task DeleteAsync(int id);
}