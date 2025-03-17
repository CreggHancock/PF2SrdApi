using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models.Spells;

public record SpellSave
{
    [BsonElement("statistic")]
    public required string Statistic { get; init; }

    [BsonElement("basic")]
    public bool? Basic { get; init; }
}
