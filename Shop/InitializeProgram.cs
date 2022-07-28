using Shop.Constants;

namespace Shop;

public static class InitializeProgram
{
    public static void Initialize(WebApplicationBuilder appBuilder)
    {
        IConfiguration config = appBuilder.Configuration;

        try
        {
            UrlConst.Initialize(config);
            PathConst.Initialize(config, appBuilder.Environment.WebRootPath);

            bool isCreate = config.GetRequiredSection("Paths")
                                  .GetRequiredSection("CreateDirectories")
                                  .Get<bool>();

            CheckDirectories(isCreate);
        }
        catch (Exception ex)
        {
            Environment.FailFast(ex.Message, ex);
        }
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
}
