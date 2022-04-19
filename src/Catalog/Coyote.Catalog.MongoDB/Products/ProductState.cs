using MongoDB.Bson;

namespace Coyote.Catalog.MongoDB.Products;

internal class ProductState
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public BsonArray? Features { get; set; }
}
