namespace Microsoft.Extensions.DependencyInjection;

using Coyote.Catalog.PostgreSQL;
using Coyote.Catalog.PostgreSQL.Products;
using Coyote.Catalog.Products.Domain;
using Microsoft.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
    public static void UsingPostgreSQL(this IServiceCollection services, string connectionString)
    {
        services.AddEntityFrameworkNpgsql().AddDbContext<CatalogDbContext>(options => options.UseNpgsql(connectionString));
        services.AddTransient<IProductRepository, ProductRepository>();
    }
}
