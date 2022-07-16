using Shop.Context;
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

    // example: pathimage = "/products/id"
    public async Task<IEnumerable<Image>> CreateImages(IFormFileCollection uploadedFiles, string pathImage)
    {
        if (uploadedFiles.Count <= 0)
        {
            throw new Exception("Sended zero images");
        }

        string[] filesNames = new string[uploadedFiles.Count];
        int iteration = 0;
        foreach (IFormFile? file in uploadedFiles)
        {
            string[] splittedType = file.ContentType.Split('/');
            string type = splittedType[0];
            if (!IsImage(type))
            {
                throw new Exception($"File '{file.FileName}' is not image");
            }

            string format = splittedType[1];
            filesNames[iteration] = $"{Guid.NewGuid()}.{format}";

            iteration++;
        }
        List<Image> images = new(uploadedFiles.Count);

        string directoryPath = $"{Program.PathToImages}{pathImage}";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        iteration = 0;
        foreach (IFormFile? file in uploadedFiles)
        {
            string fullPath = $"{directoryPath}/{filesNames[iteration]}";

            Image image = await DownloadImage(file, fullPath, filesNames[iteration], pathImage);
            images.Add(image);

            iteration++;
        }
        return images;
    }

    private async Task<Image> DownloadImage(IFormFile uploadedFile, string FullPath, string nameImage, string pathImage)
    {
        // сохраняем файл в папку Files в каталоге wwwroot
        using (FileStream? fileStream = new FileStream(FullPath, FileMode.Create))
        {
            await uploadedFile.CopyToAsync(fileStream);
        }

        Image image = new Image { Name = nameImage, Path = pathImage };
        return image;
    }

    private bool IsImage(string type)
    {
        return type.Equals("image", StringComparison.OrdinalIgnoreCase);
    }

}