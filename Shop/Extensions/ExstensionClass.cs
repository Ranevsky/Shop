using Shop.Models.Repository;

namespace Shop.Extensions;
static public class ExstensionClass
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
