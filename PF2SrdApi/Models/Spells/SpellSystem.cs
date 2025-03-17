using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace PF2SrdApi.Models.Spells;

[BsonIgnoreExtraElements]
public record SpellSystem
{
    [BsonElement("area")]
    public SpellArea? Area { get; init; }

    [BsonElement("cost")]
    public required GenericValue<string> Cost { get; init; }

    [BsonElement("level")]
    public required GenericValue<int> Level { get; init; }

    [BsonElement("counteraction")]
    public required bool Counteraction { get; init; }

    [BsonElement("damage")]
    public required Dictionary<string, SpellDamage> Damage { get; init; }

    [BsonElement("defense")]
    public required SpellDefense? Defense { get; init; }

    [BsonIgnore]
    public GenericValue<string> Description
        {
            get
            {
                var descriptionNoMarkup = Regex.Replace(this.DescriptionRaw.Value.Replace("\n", string.Empty), "<[^>]+>", string.Empty);
                var cleanedUpDescription = Regex.Replace(
                    descriptionNoMarkup,
                    "@UUID\\[.+]{.+}",
                    (match) =>
                    {
                        return Regex.Replace(match.Value.Replace("}", string.Empty), "@UUID\\[.+]{", string.Empty);
                    });
                return new GenericValue<string>
                {
                    Value = Regex.Replace(
                        cleanedUpDescription,
                        "@UUID\\[.+]",
                        (match) =>
                        {
                            return Regex.Replace(match.Value.Replace("]", string.Empty), "@UUID\\[.+\\.(?=[^.]*$)", string.Empty);
                        }),
                };
            }
        }

    [BsonElement("duration")]
    public required SpellDuration Duration { get; init; }

    [BsonElement("heightening")]
    public required SpellHeightening Heightening { get; init; }

    [BsonElement("target")]
    public required GenericValue<string> Target { get; init; }

    [BsonElement("time")]
    public required GenericValue<string> Time { get; init; }

    [BsonElement("range")]
    public required GenericValue<string> Range { get; init; }

    [BsonElement("requirements")]
    public string? Requirements { get; init; }

    [BsonElement("publication")]
    public required Publication Publication { get; init; }

    [BsonElement("traits")]
    public required SpellTraits Traits { get; init; }

    [BsonElement("description")]
    public required GenericValue<string> DescriptionRaw { get; init; }
}
