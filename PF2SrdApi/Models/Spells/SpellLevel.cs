using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models.Spells;

public record SpellLevel
{
    [BsonElement("damage")]
    public Dictionary<string, SpellDamage>? Damage { get; init; }

    [BsonElement("target")]
    public GenericValue<string>? Target { get; init; }

    [BsonElement("range")]
    public GenericValue<string>? Range { get; init; }

    [BsonElement("traits")]
    public GenericValue<string[]>? Traits { get; init; }

    [BsonElement("area")]
    public SpellAreaHeightened? Area { get; init; }

    [BsonElement("time")]
    public GenericValue<string>? Time { get; init; }
}
