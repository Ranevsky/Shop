using Microsoft.EntityFrameworkCore;

using Shop.Database;
using Shop.Models.Repository;

namespace Shop.Extensions;

public static class ExstensionClass
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    public static IServiceCollection AddApplicationContext(this IServiceCollection services)
    {
        return services.AddDbContext<ApplicationContext>(option =>
                {
                    string connection = Program.Configuration.GetConnectionString("DefaultConnection");
                    option.UseSqlite(connection);
                });
    }


}
