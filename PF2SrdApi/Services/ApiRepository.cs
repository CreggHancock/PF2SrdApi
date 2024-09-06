using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PF2SrdApi.Models;

namespace PF2SrdApi.Services;

public class ApiRepository(IMongoDatabase database)
{
    public virtual IMongoQueryable<T> Get<T>()
        where T : EntityBase, IEntity
    {
        var collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        return collection.AsQueryable();
    }

    private static string GetCollectionName(Type type)
    {
        return (string?)type.GetProperty(nameof(IEntity.TableName))?.GetValue(null) ?? throw new ArgumentOutOfRangeException(nameof(type));
    }
}
