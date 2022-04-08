using MongoDB.Bson;

namespace Coyote.Catalog.MongoDB.Products;

internal class ProductState
{
    public ProductState(Guid id, Guid typeId, string name, BsonArray features)
    {
        Id = id;
        TypeId = typeId;
        Name = name;
        Features = features;
    }

    public Guid Id { get; set; }
    public Guid TypeId { get; set; }
    public string Name { get; set; }
    public BsonArray Features { get; set; }
}
