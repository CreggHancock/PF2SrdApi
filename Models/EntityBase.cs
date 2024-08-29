using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PF2SrdApi.Models;

public abstract record EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }

    [BsonElement("index")]
    public required string Index { get; init; }
}
