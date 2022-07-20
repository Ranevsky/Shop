using Shop.Exceptions;
using Shop.Models.Product;

namespace Shop.Repositories.Interfaces;

public interface IImageRepository
{
    /// <exception cref="UploadFileIsEmptyException"></exception>
    /// <exception cref="FileIsNotImageException"></exception>
    Task<IEnumerable<Image>> CreateImagesAsync(IFormFileCollection uploadedFiles, string pathImage);
}
