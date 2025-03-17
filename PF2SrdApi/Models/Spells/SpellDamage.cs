using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models.Spells;

public record SpellDamage
{
    [BsonElement("applyMod")]
    public required bool ApplyMod { get; init; }

    [BsonElement("category")]
    public required string? Category { get; init; }

    [BsonElement("formula")]
    public required string Formula { get; init; }

    [BsonElement("kinds")]
    public required string[] Kinds { get; init; }

    [BsonElement("materials")]
    public required string[] Materials { get; init; }

    [BsonElement("type")]
    public required string Type { get; init; }
}