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

        Action<string, string> exception = (string key, string value) =>
        {
            throw new PathEndedSlashJsonException(key, value, value[^1]);
        };

        _imagePath = paths.GetRequiredSection("ImagePath").Value;
        ImagePath.EndsSlashException(nameof(ImagePath), exception);

        _imageFullPath = $"{webRootPath}{_imagePath}";

        _productPath = paths.GetRequiredSection("ProductPath").Value;
        ProductPath.EndsSlashException(nameof(ProductPath), exception);
    }
}
