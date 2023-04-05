using Template2.Server.Interfaces;

namespace Template2.Server.Endpoints;

public static class ProductEndpoints
{
    public static IApplicationBuilder MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/Product/List/{platform}", 
            async (IProductRepository repo, string platform, string? genre, string? age) =>
                await repo.GetAllAsync(platform, genre, age));

        return app;
    }
}