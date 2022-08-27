using Microsoft.EntityFrameworkCore;

using Shop.Context;
using Shop.Profiles;
using Shop.Repositories;
using Shop.Repositories.Interfaces;

namespace Shop.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    public static IServiceCollection AddApplicationContext(this IServiceCollection services, IConfiguration configuration)
    {
        DbContextOptionsBuilder option = new();
        SetOption(option);

        using (ApplicationContext db = new(option.Options))
        {
            db.Database.Migrate();
        }
        
        return services.AddDbContext<ApplicationContext>(SetOption);

        void SetOption(DbContextOptionsBuilder option)
        {
            string connection = configuration.GetConnectionString("DefaultConnection");
            option.UseSqlite(connection);
        }
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        Type[] profiles = new Type[]
        {
            typeof(ProductProfile),
            typeof(WarrantyProfile),
            typeof(ProductTypeProfile),
            typeof(ImageProfile)
        };

        return services.AddAutoMapper(profiles);
    }
}
