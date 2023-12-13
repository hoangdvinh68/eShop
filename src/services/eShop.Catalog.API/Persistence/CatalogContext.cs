using eShop.Catalog.API.Domain.Entities;
using MongoDB.Driver;

namespace eShop.Catalog.API.Persistence;

public class CatalogContext : ICatalogContext
{
    private readonly IMongoDatabase _database;
    
    public CatalogContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("MongoSettings:ConnectionString");
        var dbName = configuration.GetValue<string>("MongoSettings:DatabaseName");
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(dbName);
        CatalogContextSeed.Seed(Collection<Product>());
    }

    public IMongoCollection<T> Collection<T>() => _database.GetCollection<T>(typeof(T).Name);
}