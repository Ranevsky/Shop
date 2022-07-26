using Shop.Middlewares;

namespace Shop.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseActionResultException(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ActualResultExceptionMiddleware>();
    }
}
