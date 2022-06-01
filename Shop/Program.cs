using System.Text.Json.Nodes;

using Microsoft.Extensions.FileProviders;

using Shop.Extensions;


namespace Shop;

internal sealed class Program
{
    public static IConfiguration Configuration = null!;
    public static string PathToImages = null!;
    public const string ImageUrl = "/image";
    public static string applicationUrl = null!;
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

        applicationUrl = applicationUr;
    }

    private static void GetPathToImages()
    {
        string pathToImages = Configuration.GetValue<string>("PathToImages");
        if (string.IsNullOrEmpty(pathToImages))
        {
            throw new Exception("Enter in 'appsetings.json' path to images");
        }
        PathToImages = Directory.GetCurrentDirectory() + pathToImages;
        if (!Directory.Exists(PathToImages))
        {
            // If the folder is not created and suddenly it is expected that there will be files
            throw new Exception($"Not exist '{PathToImages}', please create directory");
        }

    }

    private static void Main(string[] args)
    {
        WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
        Configuration = builder.Configuration;
        GetPathToImages();
        GetApplicationUrl();

        // Services

        builder.Services.AddCors();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddUnitOfWork();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddApplicationContext();
        builder.Services.AddControllers();

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

