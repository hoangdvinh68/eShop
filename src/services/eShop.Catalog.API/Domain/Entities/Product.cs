using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eShop.Catalog.API.Domain.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;

    public string Category { get; set; } = default!;

    public string Summary { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string ImageFile { get; set; } = default!;

    public int Price { get; set; }
}