using Shop.Exceptions.Json;
using Shop.Extensions;

namespace Shop.Constants;

public static class UrlConst
{
    /// <summary>
    /// Example: https://localhost:7287
    /// </summary>
    public static string ApplicationUrl => _applicationUrl;

    /// <summary>
    /// Example: /image
    /// </summary>
    public static string ImageUrl => _imageUrl;

    /// <summary>
    /// Example: https://localhost:7287/image
    /// </summary>
    public static string FullImageUrl => _fullImageUrl;

    private static string _applicationUrl = null!;
    private static string _imageUrl = null!;
    private static string _fullImageUrl = null!;

    public static void Initialize(IConfiguration config)
    {
        IConfigurationSection urls = config.GetRequiredSection("Urls");

        _applicationUrl = urls.Value.Split(';')[0];
        ApplicationUrl.EndsSlashException(nameof(ApplicationUrl), Exception);

        _imageUrl = urls.GetRequiredSection("ImageUrl").Value;
        ImageUrl.EndsSlashException(nameof(ImageUrl), Exception);

        _fullImageUrl = ApplicationUrl + ImageUrl;
    }
    private static void Exception(string key, string value)
    {
        throw new UrlEndedSlashJsonException(key, value, value[^1]);
    }
}
