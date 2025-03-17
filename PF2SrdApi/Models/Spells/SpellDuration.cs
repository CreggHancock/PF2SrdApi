using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models.Spells;

public record SpellDuration : GenericValue<string>
{
    [BsonElement("sustained")]
    public required bool Sustained { get; init; }
}