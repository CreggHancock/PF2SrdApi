using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models;

[BsonIgnoreExtraElements]
public record GenericValue<T>
{
    [BsonElement("value")]
    public required T Value { get; init; }
}
