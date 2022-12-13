using Coyote.Catalog.Products.Domain;

namespace Coyote.Catalog.Products.Application;

public class ProductService : IProductService
{
    private readonly IProductRepository repository;

    public ProductService(IProductRepository repository)
    {
        this.repository = repository;
    }

    private static ProductOutput ToOutput(Product product)
    {
        return new ProductOutput
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Features = product.Features.ToDictionary(f => f.Name, f => f.Value)
        };
    }

    public async Task<IEnumerable<ProductOutput>> ListAsync()
    {
        var products = await repository.ListAsync();
        return products.Select(p => ToOutput(p));
    }

    public async Task<ProductOutput?> FindAsync(Guid id)
    {
        var product = await repository.FindAsync(id);
        return product == default ? default : ToOutput(product);
    }

    public async Task<ProductOutput> CreateAsync(ProductInput input)
    {
        var features = input.Features.Select(f => new ProductFeature(f.Key, f.Value));
        var product = new Product(Guid.NewGuid(), input.Name!, input.Price!.Value, input.Description, features);
        await repository.AddAsync(product);
        return ToOutput(product);
    }

    public async Task UpdateAsync(Guid id, ProductInput input)
    {
        var product = await repository.FindAsync(id) ?? throw new ArgumentException("Product not found.", nameof(id));
        product.Name = input.Name!;
        product.Price = input.Price!.Value;
        product.Description = input.Description;
        var features = input.Features.Select(f => new ProductFeature(f.Key, f.Value));
        product.Features = features;
        await repository.UpdateAsync(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.DeleteAsync(id);
    }
}
