using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class MongoService
{
    private readonly IMongoCollection<Cart> _cartsCollection;

    public MongoService(
        IOptions<MongoDatabaseSettings> cartStoreDatabaseSettings, IConfiguration configuration)
    {
        var mongoClient = new MongoClient(
            String.IsNullOrEmpty (Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"))?
                                configuration.GetConnectionString("MONGO_CONNECTION_STRING"):
                                Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING")
            );

        var mongoDatabase = mongoClient.GetDatabase(
            String.IsNullOrEmpty (Environment.GetEnvironmentVariable("MONGO_DATABASE"))?
                                configuration.GetConnectionString("MONGO_DATABASE"):
                                Environment.GetEnvironmentVariable("MONGO_DATABASE")    
        );
        

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        
        _cartsCollection = mongoDatabase.GetCollection<Cart>(
            String.IsNullOrEmpty (Environment.GetEnvironmentVariable("MONGO_CARTS_COLLECTION"))?
                                configuration.GetConnectionString("MONGO_CARTS_COLLECTION"):
                                Environment.GetEnvironmentVariable("MONGO_CARTS_COLLECTION") 
                                );
       
    }

    public async Task<List<Cart>> GetAsync() =>
        await _cartsCollection.Find(_ => true).ToListAsync();

    public async Task<Cart?> GetAsync(Guid id) =>
        await _cartsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Cart n) =>
        await _cartsCollection.InsertOneAsync(n);

    public async Task UpdateAsync(Guid id, Cart u) =>
        await _cartsCollection.ReplaceOneAsync(x => x.Id == id, u);

    public async Task RemoveAsync(Guid id) =>
        await _cartsCollection.DeleteOneAsync(x => x.Id == id);
}