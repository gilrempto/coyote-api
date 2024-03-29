﻿using Coyote.Catalog.Products.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Coyote.Catalog.MongoDB.Products;

public class ProductRepository : IProductRepository
{
    private const string COLLECTION_NAME = "Products";
    private readonly IMongoCollection<ProductState> collection;

    public ProductRepository(IOptions<MongoDBOptions> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        var database = client.GetDatabase(options.Value.DatabaseName);
        collection = database.GetCollection<ProductState>(COLLECTION_NAME);
    }

    private static Product ToProduct(ProductState state)
    {
        var features = BsonSerializer.Deserialize<ProductFeature[]>(state.Features.ToJson());
        return new Product(state.Id, state.Name, state.Price, state.Description, features);
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        var states = await collection.Find(p => true).ToListAsync();
        return states.Select(s => ToProduct(s));
    }

    public async Task<Product?> FindAsync(Guid id)
    {
        var state = await collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        return state == default ? default : ToProduct(state);
    }

    public async Task AddAsync(Product product)
    {
        var json = JsonConvert.SerializeObject(product.Features);
        var features = BsonSerializer.Deserialize<BsonArray>(json);
        var state = new ProductState
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Features = features
        };
        await collection.InsertOneAsync(state);
    }

    public async Task UpdateAsync(Product product)
    {
        var state = await collection.Find(p => p.Id == product.Id).FirstOrDefaultAsync();
        state.Name = product.Name;
        state.Description = product.Description;
        state.Price = product.Price;
        var json = JsonConvert.SerializeObject(product.Features);
        var features = BsonSerializer.Deserialize<BsonArray>(json);
        state.Features = features;
        await collection.ReplaceOneAsync(p => p.Id == product.Id, state);
    }

    public async Task DeleteAsync(Guid id)
    {
        await collection.DeleteOneAsync(p => p.Id == id);
    }
}
