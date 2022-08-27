using Shop.Exceptions.Json;
using Shop.Extensions;

namespace Shop.Constants;

public static class PathConst
{
    /// <summary>
    /// Example: /images
    /// </summary>
    public static string ImagePath => _imagePath;
    /// <summary>
    /// Example: C:/.../wwwroot/images
    /// </summary>
    public static string ImageFullPath => _imageFullPath;
    /// <summary>
    /// Example: /products
    /// </summary>
    public static string ProductPath => _productPath;

    private static string _imagePath = null!;
    private static string _imageFullPath = null!;
    private static string _productPath = null!;

    public static void Initialize(IConfiguration config, string webRootPath)
    {
        IConfigurationSection paths = config.GetRequiredSection("Paths");

        _imagePath = paths.GetRequiredSection("ImagePath").Value;
        ImagePath.EndsSlashException(nameof(ImagePath), Exception);

        _imageFullPath = $"{webRootPath}{_imagePath}";

        _productPath = paths.GetRequiredSection("ProductPath").Value;
        ProductPath.EndsSlashException(nameof(ProductPath), Exception);

        bool isCreate = paths.GetRequiredSection("CreateDirectories").Get<bool>();
        CheckDirectories(isCreate);
    }
    public static void CheckDirectories(bool isCreate)
    {
        string baseDirectory = PathConst.ImageFullPath;

        Action<string> action = isCreate
            ? path => Directory.CreateDirectory(path)
            : path => throw new DirectoryNotFoundException($"Directory '{path}' not exist");

        Check(baseDirectory);

        string otherDirectory = $"{baseDirectory}{PathConst.ProductPath}";
        Check(otherDirectory);

        void Check(string path)
        {
            if (!Directory.Exists(path))
            {
                action.Invoke(path);
            }
        }
    }
    private static void Exception(string key, string value)
    {
        throw new PathEndedSlashJsonException(key, value, value[^1]);
    }
}
