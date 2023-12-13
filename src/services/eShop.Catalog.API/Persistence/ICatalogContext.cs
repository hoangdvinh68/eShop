using MongoDB.Driver;

namespace eShop.Catalog.API.Persistence;

public interface ICatalogContext
{
    IMongoCollection<T> Collection<T>();
}