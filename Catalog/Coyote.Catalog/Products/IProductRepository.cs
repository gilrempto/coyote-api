namespace Coyote.Catalog.Products
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> ListAsync();
        public Task<Product?> FindAsync(Guid id);
        public Task AddAsync(Product product);
        public Task UpdateAsync(Product product);
        public Task DeleteAsync(Guid id);
    }
}
