using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models.Spells;

public record SpellArea : GenericValue<int>
{
    [BsonElement("details")]
    public required string Details { get; init; }

    [BsonElement("type")]
    public required string Type { get; init; }
}
