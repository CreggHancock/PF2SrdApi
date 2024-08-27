using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PF2SrdApi.Models;

namespace PF2SrdApi.Services;

public class ApiService
{
    private readonly IMongoDatabase database;

    public ApiService(
        IOptions<DatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        this.database = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);
    }

    public async Task<ResultsWithCount<T>> GetAsync<T>() where T : EntityBase, IEntity
    {
        var collecton = this.database.GetCollection<T>(GetCollectionName(typeof(T)));
        var results = await collecton.Find(_ => true).ToListAsync();
        return new ResultsWithCount<T>
        { 
            Count = results.Count,
            Results = results,
        };
    }
        
    public async Task<T?> GetAsync<T>(string index) where T : EntityBase, IEntity
    {
        var collecton = this.database.GetCollection<T>(GetCollectionName(typeof(T)));
        return await collecton.Find(x => x.Index == index).FirstOrDefaultAsync();
    }

    private static string GetCollectionName(Type type)
    {
        return (string?)type.GetProperty(nameof(IEntity.TableName))?.GetValue(null) ?? throw new ArgumentOutOfRangeException(nameof(type));
    }
}
