using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PF2SrdApi.Models;

[BsonIgnoreExtraElements]
public abstract record EntityBase
{
    [BsonElement("_id")]
    public required string Id { get; init; }
}
