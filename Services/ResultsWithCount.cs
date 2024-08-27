namespace PF2SrdApi.Services;

public record ResultsWithCount<T>
{
    public required int Count { get; init; }

    public required IReadOnlyCollection<T> Results { get; init; }
}
