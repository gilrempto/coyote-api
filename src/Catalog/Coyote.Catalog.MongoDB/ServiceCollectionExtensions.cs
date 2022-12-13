using Coyote.Catalog.MongoDB;
using Coyote.Catalog.MongoDB.Products;
using Coyote.Catalog.Products.Domain;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void UsingMongoDB(this IServiceCollection services, string connectionString)
    {
        services.Configure<MongoDBOptions>(options => options.ConnectionString = connectionString);
        services.AddSingleton<IProductRepository, ProductRepository>();
    }
}
