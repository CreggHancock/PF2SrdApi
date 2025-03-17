using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models.Spells;

[BsonIgnoreExtraElements]
public record SpellAreaHeightened : GenericValue<int>
{
    [BsonElement("type")]
    public required string Type { get; init; }
}
