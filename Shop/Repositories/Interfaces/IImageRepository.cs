using Shop.Models.Product;

namespace Shop.Repositories.Interfaces;

public interface IImageRepository
{
    Task<IEnumerable<Image>> CreateImages(IFormFileCollection uploadedFiles, string pathImage);
}
