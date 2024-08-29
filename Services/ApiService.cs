using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PF2SrdApi.Models;
using PF2SrdApi.Common;

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

    public async Task<ResultsWithCount<MonsterMinimal>> GetMonsters(int? level)
    {
        var collection = this.database.GetCollection<Monster>(Monster.TableName);
        var results = await collection.Find(m => level == null || m.Level == level).ToListAsync();
        return results.Select(m => new MonsterMinimal { Index = m.Index, Name = m.Name, Url = m.Url,}).ToResultsWithCount();
    }

    public async Task<ResultsWithCount<T>> GetAsync<T>() where T : EntityBase, IEntity
    {
        var collecton = this.database.GetCollection<T>(GetCollectionName(typeof(T)));
        var results = await collecton.Find(_ => true).ToListAsync();
        return results.ToResultsWithCount();
    }
        
    public async Task<T?> GetAsync<T>(string index) where T : EntityBase, IEntity
    {
        var collection = this.database.GetCollection<T>(GetCollectionName(typeof(T)));
        return await collection.Find(x => x.Index == index).FirstOrDefaultAsync();
    }

    private static string GetCollectionName(Type type)
    {
        return (string?)type.GetProperty(nameof(IEntity.TableName))?.GetValue(null) ?? throw new ArgumentOutOfRangeException(nameof(type));
    }
}
