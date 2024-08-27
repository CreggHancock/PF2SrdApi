using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PF2SrdApi.Models;

public abstract record EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("index")]
    public required string Index { get; set; }
}
