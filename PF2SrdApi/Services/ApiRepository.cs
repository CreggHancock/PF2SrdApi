using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PF2SrdApi.Models;

namespace PF2SrdApi.Services;

public class ApiRepository
{
    private readonly IMongoDatabase database;

    public ApiRepository(
    IOptions<DatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        this.database = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);
    }

    public IMongoQueryable<T> Get<T>()
        where T : EntityBase, IEntity
    {
        var collection = this.database.GetCollection<T>(GetCollectionName(typeof(T)));
        return collection.AsQueryable();
    }

    private static string GetCollectionName(Type type)
    {
        return (string?)type.GetProperty(nameof(IEntity.TableName))?.GetValue(null) ?? throw new ArgumentOutOfRangeException(nameof(type));
    }
}
