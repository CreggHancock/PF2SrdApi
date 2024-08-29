using PF2SrdApi.Services;

namespace PF2SrdApi.Common;

public static class CollectionExtensions
{
    public static ResultsWithCount<T> ToResultsWithCount<T>(this IEnumerable<T> results)
    {
        return new ResultsWithCount<T>
        {
            Count = results.Count(),
            Results = results,
        };
    }
}
