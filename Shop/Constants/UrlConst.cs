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

    private static string _applicationUrl = null!;
    private static string _imageUrl = null!;

    public static void Initialize(IConfiguration config)
    {
        IConfigurationSection urls = config.GetRequiredSection("Urls");

        Action<string, string> exception = (string key, string value) =>
        {
            throw new UrlEndedSlashJsonException(key, value, value[value.Length - 1]);
        };

        _applicationUrl = urls.Value.Split(';')[0];
        ApplicationUrl.EndsSlashException(nameof(ApplicationUrl), exception);

        _imageUrl = urls.GetRequiredSection("ImageUrl").Value;
        ImageUrl.EndsSlashException(nameof(ImageUrl), exception);
    }
}
