using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models.Spells;

[BsonIgnoreExtraElements]
public record SpellTraits : GenericValue<string[]>
{
    [BsonElement("rarity")]
    public required string Rarity { get; init; }

    [BsonElement("traditions")]
    public required string[] Traditions { get; init; }
}
