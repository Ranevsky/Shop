using Shop.Constants;
using Shop.Context;
using Shop.Exceptions;
using Shop.Models.Product;
using Shop.Repositories.Interfaces;

namespace Shop.Repositories;

public sealed class ImageRepository : IImageRepository
{
    public readonly ApplicationContext Db;
    public ImageRepository(ApplicationContext db)
    {
        Db = db;
    }

    // example: pathimage = "/products/id
    public async Task<IEnumerable<Image>> CreateImagesAsync(IFormFileCollection uploadedFiles, string pathImage)
    {
        if (uploadedFiles.Count < 1)
        {
            throw new UploadFileIsEmptyException();
        }

        string[] filesNames = new string[uploadedFiles.Count];
        int iteration = 0;
        foreach (IFormFile file in uploadedFiles)
        {
            string[] splittedType = file.ContentType.Split('/');
            string type = splittedType[0];
            if (!IsImage(type))
            {
                throw new FileIsNotImageException(file.FileName);
            }

            string format = splittedType[1];
            filesNames[iteration] = $"{Guid.NewGuid()}.{format}";

            iteration++;
        }
        List<Image> images = new(uploadedFiles.Count);

        string directoryPath = $"{PathConst.ImageFullPath}{pathImage}";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        iteration = 0;
        foreach (IFormFile file in uploadedFiles)
        {
            string fullPath = $"{directoryPath}/{filesNames[iteration]}";

            Image image = await DownloadImageAsync(file, fullPath, filesNames[iteration], pathImage);
            images.Add(image);

            iteration++;
        }
        return images;
    }

    private static async Task<Image> DownloadImageAsync(IFormFile uploadedFile, string FullPath, string nameImage, string pathImage)
    {
        using (FileStream fileStream = new(FullPath, FileMode.Create))
        {
            await uploadedFile.CopyToAsync(fileStream);
        }

        Image image = new() { Name = nameImage, Path = pathImage };
        return image;
    }

    private static bool IsImage(string type)
    {
        return type.Equals("image", StringComparison.OrdinalIgnoreCase);
    }

}