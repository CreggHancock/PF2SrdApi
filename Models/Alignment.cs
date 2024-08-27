using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models;

public record Alignment : EntityBase, IEntity
{
    public static string TableName => "alignments";

    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("abbreviation")]
    public required string Abbreviation { get; set; }

    [BsonElement("desc")]
    public required string Desc { get; set; }

    [BsonElement("url")]
    public required string Url { get; set; }
}
