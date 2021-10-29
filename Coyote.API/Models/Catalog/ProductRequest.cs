namespace Coyote.API.Models.Catalog
{
    public class ProductRequest
    {
        public ProductRequest(Guid typeId, string name)
        {
            TypeId = typeId;
            Name = name;
        }

        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public IDictionary<string, object> Features { get; set; } = new Dictionary<string, object>();
    }
}
