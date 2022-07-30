using Shop.Constants;
using Shop.Exceptions;
using Shop.Models.Product;

namespace Shop.Models;

public static class ImageCreator<TImage> where TImage : Image, new()
{
    // example: pathimage = "id" -> final directory
    // example: imageDirectory = "/products" -> directory before wwwroot/images
    /// <exception cref="UploadFileIsEmptyException"></exception>
    /// <exception cref="FileIsNotImageException"></exception>
    public static async Task<IEnumerable<TImage>> CreateAsync(IFormFileCollection uploadedFiles, string? imageDirectory, string pathImage)
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
        List<TImage> images = new(uploadedFiles.Count);

        string directoryPath = $"{PathConst.ImageFullPath}{imageDirectory?.ToString() ?? ""}/{pathImage}";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        iteration = 0;
        foreach (IFormFile file in uploadedFiles)
        {
            string fullPath = $"{directoryPath}/{filesNames[iteration]}";

            TImage image = await DownloadImageAsync(file, fullPath, filesNames[iteration], pathImage);
            images.Add(image);

            iteration++;
        }
        return images;
    }

    private static async Task<TImage> DownloadImageAsync(IFormFile uploadedFile, string FullPath, string nameImage, string pathImage)
    {
        using (FileStream fileStream = new(FullPath, FileMode.Create))
        {
            await uploadedFile.CopyToAsync(fileStream);
        }

        TImage image = new() { Name = nameImage, Path = pathImage };
        return image;
    }

    private static bool IsImage(string type)
    {
        return type.Equals("image", StringComparison.OrdinalIgnoreCase);
    }

}