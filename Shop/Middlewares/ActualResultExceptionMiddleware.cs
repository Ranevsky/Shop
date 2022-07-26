using Shop.Exceptions;

namespace Shop.Middlewares;

public sealed class ActualResultExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ActualResultExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (ActionResultException ex)
        {
            Shop.Exceptions.Models.ErrorModel model = ex.ErrorModel;

            context.Response.StatusCode = model.Status;
            await context.Response.WriteAsJsonAsync(model);
        }
    }
}
