namespace Coyote.Catalog.Products;

public class ProductOutput
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public IDictionary<string, object> Features { get; set; } = new Dictionary<string, object>();
}
