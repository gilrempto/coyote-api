namespace Coyote.Catalog.MongoDB;

public class MongoDBOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = "Catalog";
}
