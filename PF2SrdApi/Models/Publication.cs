using MongoDB.Bson.Serialization.Attributes;

namespace PF2SrdApi.Models;

public record Publication
{
    [BsonElement("license")]
    public required string License { get; init; }

    [BsonElement("remaster")]
    public required bool Remaster { get; init; }

    [BsonElement("title")]
    public required string Title { get; init; }
}
