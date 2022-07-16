using Shop.Models.Catalog;
using Shop.Models.Product;

namespace Shop.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IQueryable<Product>> Paging(SortAndFilter model);
    Task AddImages(int id, IFormFileCollection uploadedFiles, IImageRepository imageRepository);
    Task DeleteImages(int productId, params int[] imagesId);
    Task Delete(int id);
}