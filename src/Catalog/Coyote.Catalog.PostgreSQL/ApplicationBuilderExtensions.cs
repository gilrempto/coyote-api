namespace Microsoft.AspNetCore.Builder;

using Coyote.Catalog.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static void InitializeCatalogDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
        dbContext.Database.EnsureCreated();
    }
}
