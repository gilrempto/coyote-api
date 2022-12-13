using Coyote.Catalog.Products.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Coyote.Catalog.PostgreSQL.Products;

internal class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext db;

    public ProductRepository(CatalogDbContext db)
    {
        this.db = db;
    }

    private static Product ToProduct(ProductState state)
    {
        var dictionary = JsonConvert.DeserializeObject<IDictionary<string, object>>(state.Features);
        var features = dictionary!.Select(d => new ProductFeature(d.Key, d.Value));
        return new Product(state.Id, state.Name, state.Price, state.Description, features);
    }

    private static string ToJson(IEnumerable<ProductFeature> features)
    {
        var dictionary = features.ToDictionary(f => f.Name, f => f.Value);
        return JsonConvert.SerializeObject(dictionary);
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        var states = await db.Products.ToListAsync();
        return states.Select(s => ToProduct(s));
    }

    public async Task<Product?> FindAsync(Guid id)
    {
        var state = await db.Products.SingleOrDefaultAsync(p => p.Id == id);
        return state == default ? default : ToProduct(state);
    }

    public async Task AddAsync(Product product)
    {

        var state = new ProductState
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Features = ToJson(product.Features)
        };
        db.Products.Add(state);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        var state = await db.Products.SingleAsync(p => p.Id == product.Id);
        state.Name = product.Name;
        state.Description = product.Description;
        state.Price = product.Price;
        state.Features = ToJson(product.Features);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var state = await db.Products.SingleAsync(p => p.Id == id);
        db.Products.Remove(state);
        await db.SaveChangesAsync();
    }
}
