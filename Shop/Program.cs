using Microsoft.Extensions.FileProviders;

using Shop.Constants;
using Shop.Extensions;

namespace Shop;

internal sealed class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            ContentRootPath = Directory.GetCurrentDirectory(),
            WebRootPath = "wwwroot",
            Args = args
        });

        // Services

        builder.Services.AddCors()
            .AddSwaggerGen(opt =>
            {
                opt.IncludeXmlComments($"{Directory.GetCurrentDirectory()}/Documentation.XML");
            })
            .AddAutoMapper()
            .AddUnitOfWork()
            .AddEndpointsApiExplorer()
            .AddApplicationContext(builder.Configuration)
            .AddControllers();

        WebApplication app = builder.Build();
        
        InitializeProgram.Initialize(app);

        // middlewares

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(PathConst.ImageFullPath),
            RequestPath = new PathString(UrlConst.ImageUrl)
        });
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(builder => builder.AllowAnyOrigin());
        app.UseActionResultException();
        app.Run();
    }
}

