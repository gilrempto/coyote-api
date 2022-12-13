namespace Coyote.Catalog.Products.Domain;

public class Product
{
    public Product(Guid id, string name, decimal price, string? description, IEnumerable<ProductFeature> features)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        Features = features;
    }

    public Guid Id { get; private set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public IEnumerable<ProductFeature> Features { get; set; } = Array.Empty<ProductFeature>();
}
