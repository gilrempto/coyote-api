using Coyote.Catalog.Products;
using Coyote.Catalog.Products.Application.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCatalog(this IServiceCollection services, Action<IServiceCollection> configureServices)
    {
        services.AddTransient<IProductService, ProductService>();
        configureServices.Invoke(services);
        return services;
    }
}
