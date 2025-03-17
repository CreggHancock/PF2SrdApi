using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models.Spells;

public record SpellHeightening
{
    [BsonElement("damage")]
    public Dictionary<string, string>? Damage { get; init; }

    [BsonElement("interval")]
    public int? Interval { get; init; }

    [BsonElement("levels")]
    public Dictionary<string, SpellLevel>? Levels { get; init; }

    [BsonElement("area")]
    public int? Area { get; init; }

    [BsonElement("type")]
    public required string Type { get; init; }
}
