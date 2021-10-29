namespace Coyote.API.Models.Catalog
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string? Name { get; set; }
        public IDictionary<string, object> Features { get; set; } = new Dictionary<string, object>();
    }
}
