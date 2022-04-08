using Coyote.Catalog.Products.Domain.Model;

namespace Coyote.Catalog.Products.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;

        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        private static ProductOutput ToResponse(Product product)
        {
            return new ProductOutput
            {
                Id = product.Id,
                TypeId = product.TypeId,
                Name = product.Name,
                Features = product.Features.ToDictionary(f => f.Name, f => f.Value)
            };
        }

        public async Task<IEnumerable<ProductOutput>> ListAsync()
        {
            var products = await repository.ListAsync();
            return products.Select(p => ToResponse(p));
        }

        public async Task<ProductOutput?> FindAsync(Guid id)
        {
            var product = await repository.FindAsync(id);
            return product == default ? default : ToResponse(product);
        }

        public async Task<ProductOutput> CreateAsync(ProductInput request)
        {
            var features = request.Features.Select(f => new ProductFeature(f.Key, f.Value));
            var product = new Product(Guid.NewGuid(), request.TypeId, request.Name, features);
            await repository.AddAsync(product);
            return ToResponse(product);
        }

        public async Task UpdateAsync(Guid id, ProductInput request)
        {
            var product = await repository.FindAsync(id) ?? throw new ArgumentException("Product not found.", nameof(id));
            product.Name = request.Name;
            product.TypeId = request.TypeId;
            var features = request.Features.Select(f => new ProductFeature(f.Key, f.Value));
            product.Features = features;
            await repository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await repository.DeleteAsync(id);
        }
    }
}
