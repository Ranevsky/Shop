using Shop.Models.Product;

namespace Shop.Repositories.Interfaces;

public interface IImageRepository
{
    Task<IEnumerable<Image>> CreateImagesAsync(IFormFileCollection uploadedFiles, string pathImage);
}
