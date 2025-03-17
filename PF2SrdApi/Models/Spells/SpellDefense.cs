using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models.Spells;

public record SpellDefense
{
    [BsonElement("save")]
    public SpellSave? Save { get; init; }

    [BsonElement("passive")]
    public SpellSave? Passive { get; init; }
}
