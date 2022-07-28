using Microsoft.EntityFrameworkCore;

using Shop.Context;
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
        return services.AddDbContext<ApplicationContext>(option =>
                {
                    string connection = configuration.GetConnectionString("DefaultConnection");
                    option.UseSqlite(connection);
                });
    }


}
