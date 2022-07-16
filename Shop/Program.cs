using System.Text.Json.Nodes;

using Microsoft.Extensions.FileProviders;

using Shop.Extensions;
using Shop.Profiles;


namespace Shop;

internal sealed class Program
{
    public static IConfiguration Configuration { get; private set; } = null!;
    /// <summary>
    /// Example: C:/.../wwwroot/images
    /// </summary>
    public static string PathToImages { get; private set; } = null!;
    /// <summary>
    /// Example: https://localhost:7287
    /// </summary>
    public static string ApplicationUrl { get; private set; } = null!;

    public const string ImageUrl = "/image";
    public const string ImageDirectory = "/images";
    public const string ProductDirectory = "/products";

    private static void GetApplicationUrl()
    {
        string path = $"{Directory.GetCurrentDirectory()}/Properties/launchSettings.json";
        if (!File.Exists(path))
        {
            throw new Exception("'LaunchSettings.json' not found");
        }

        string text = File.ReadAllText(path);

        JsonNode js = JsonNode.Parse(text)!;
        string? applicationUr = js["profiles"]?["Shop"]?["applicationUrl"]?.ToString().Split(';')[0]
            ?? throw new Exception("file 'launchSettings.json' configured incorrectly, path not found (profiles -> Shop -> applicationUrl)");

        ApplicationUrl = applicationUr;
    }
    private static void GetPathToImages(string webRootPath)
    {
        PathToImages = $"{webRootPath}{ImageDirectory}";
        if (!Directory.Exists(PathToImages))
        {
            // If the folder is not created and suddenly it is expected that there will be files
            throw new Exception($"Not exist '{PathToImages}', please create directory");
        }
    }
    private static string GetXmlCommentsPath()
    {
        return $"{Directory.GetCurrentDirectory()}/Documentation.XML";
    }

    private static void Main(string[] args)
    {
        WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

        Configuration = builder.Configuration;
        GetPathToImages(builder.Environment.WebRootPath);
        GetApplicationUrl();
        //return;
        // Services

        builder.Services.AddCors()
            .AddSwaggerGen(opt =>
            {
                opt.IncludeXmlComments(GetXmlCommentsPath());
            })
            .AddAutoMapper(typeof(ProductProfile))
            .AddUnitOfWork()
            .AddEndpointsApiExplorer()
            .AddApplicationContext()
            .AddControllers();

        WebApplication? app = builder.Build();

        // middlewares

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(PathToImages),
            RequestPath = new PathString(ImageUrl)
        });
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(builder => builder.AllowAnyOrigin());
        app.Run();
    }
}

