using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models;

[BsonIgnoreExtraElements]
public record Monster : EntityBase, IEntity
{
    public static string TableName => "monsters";

    [BsonElement("name")]
    public required string Name { get; init; }

    [BsonElement("url")]
    public required string Url { get; set; }
}
