using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models.Spells;

[Node(
    IdField = nameof(Id),
    NodeResolverType = typeof(SpellNodeResolver),
    NodeResolver = nameof(SpellNodeResolver.ResolveAsync))]
[BsonIgnoreExtraElements]
public record Spell : EntityBase, IEntity
{
    public static string TableName => "spells";

    [BsonElement("name")]
    public required string Name { get; init; }

    [BsonElement("system")]
    public required SpellSystem System { get; init; }

    [BsonElement("type")]
    public required string Type { get; init; }
}
