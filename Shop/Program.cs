using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Linq;
using Shop.Extensions;
using Shop.Database;
namespace Shop;

class Program
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
        var json = JObject.Parse(text);
        applicationUrl = json["profiles"]!["Shop"]!["applicationUrl"]!.ToString().Split(';')[0];
    }
    private static void GetPathToImages()
    {
        string pathToImages = Configuration.GetValue<string>("PathToImages");
        if (pathToImages == "")
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
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Configuration = builder.Configuration;
        GetPathToImages();
        GetApplicationUrl();
        // Services
        
        
        builder.Services
            .AddCors()
            .AddSwaggerGen()
            .AddAutoMapper(typeof(Program))
            .AddUnitOfWork()
            .AddEndpointsApiExplorer()
            .AddDbContext<ApplicationContext>(option =>
            {
                string connection = Configuration.GetConnectionString("DefaultConnection");
                option.UseSqlite(connection);
            })
            .AddControllers();

        var app = builder.Build();
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

