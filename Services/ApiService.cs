namespace PF2SrdApi.Services;

using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PF2SrdApi.Common;
using PF2SrdApi.Models;

public class ApiService
{
    private readonly IMongoDatabase database;
    private readonly IMapper mapper;

    public ApiService(
        IOptions<DatabaseSettings> bookStoreDatabaseSettings,
        IMapper mapper)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        this.database = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);
        this.mapper = mapper;
    }

    public async Task<ResultsWithCount<MonsterMinimal>> GetMonsters(int? level)
    {
        var results = await this.Get<Monster>()
            .Where(m => level == null | m.Level == level)
            .ToListAsync();

        return results.Select(this.mapper.Map<MonsterMinimal>).ToResultsWithCount();
    }

    public async Task<ResultsWithCount<T>> GetAsync<T>()
        where T : EntityBase, IEntity
    {
        var results = await this.Get<T>().ToListAsync();
        return results.ToResultsWithCount();
    }

    public async Task<T?> GetAsync<T>(string index)
        where T : EntityBase, IEntity
    {
        return await this.Get<T>().FirstOrDefaultAsync(x => x.Index == index);
    }

    private static string GetCollectionName(Type type)
    {
        return (string?)type.GetProperty(nameof(IEntity.TableName))?.GetValue(null) ?? throw new ArgumentOutOfRangeException(nameof(type));
    }

    private IMongoQueryable<T> Get<T>()
        where T : EntityBase, IEntity
    {
        var collection = this.database.GetCollection<T>(GetCollectionName(typeof(T)));
        return collection.AsQueryable();
    }
}
