using Microsoft.EntityFrameworkCore;

using Shop.Constants;
using Shop.Context;

namespace Shop;

public static class InitializeProgram
{
    public static void Initialize(WebApplication app)
    {
        IConfiguration config = app.Configuration;

        try
        {
            UrlConst.Initialize(config);
            PathConst.Initialize(config, app.Environment.WebRootPath);
        }
        catch (Exception ex)
        {
            Environment.FailFast(ex.Message, ex);
        }
    }
}