namespace PF2SrdApi.Services;

public record ResultsWithCount<T>
{
    public required int Count { get; init; }

    public required IEnumerable<T> Results { get; init; }
}
