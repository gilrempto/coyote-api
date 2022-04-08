namespace Coyote.Catalog.Products;

public interface IProductService
{
    Task<ProductOutput> CreateAsync(ProductInput request);
    Task DeleteAsync(Guid id);
    Task<ProductOutput?> FindAsync(Guid id);
    Task<IEnumerable<ProductOutput>> ListAsync();
    Task UpdateAsync(Guid id, ProductInput request);
}
