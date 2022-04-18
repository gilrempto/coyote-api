namespace Coyote.Catalog.Products;

public class ProductInput
{
    public Guid TypeId { get; set; }
    public string Name { get; set; }
    public IDictionary<string, object> Features { get; set; } = new Dictionary<string, object>();
}
