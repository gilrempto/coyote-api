namespace Coyote.Catalog.Products
{
    public class Product
    {
        public Product(Guid id, Guid typeId, string name, IEnumerable<ProductFeature> features)
        {
            Id = id;
            TypeId = typeId;
            Name = name;
            Features = features;
        }

        public Guid Id { get; private set; }
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductFeature> Features { get; set; } = Array.Empty<ProductFeature>();
    }
}
