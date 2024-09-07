using AutoMapper;
using EphemeralMongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PF2SrdApi.Models;
using PF2SrdApi.Services;
using System.Text;

namespace PF2SrdApi.Tests.Services;

public abstract class TestBase
{
    protected IMongoRunner CreateMongoRunner()
    {
        var mongoOptions = new MongoRunnerOptions
        {
            BinaryDirectory = GetMongoBinaryDirectory(),
        };
        return MongoRunner.Run(mongoOptions);
    }

    protected async Task<ApiRepository> CreateApiRepository<T>(
        IMongoRunner runner,
        string tableName,
        IEnumerable<T> initialObjects)
    {
        var database = await GetDatabase(tableName, runner);
        var collection = database.GetCollection<T>(tableName);
        await collection.InsertManyAsync(initialObjects);
        return new ApiRepository(database);
    }

    protected static IMapper GetTestMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });

        return config.CreateMapper();
    }

    protected static async Task<IMongoDatabase> GetDatabase(string tableName, IMongoRunner runner)
    {
        var database = new MongoClient(runner.ConnectionString).GetDatabase("default");
        await database.CreateCollectionAsync(tableName);
        return database;
    }

    protected static string GetMongoBinaryDirectory()
    {
        var rootPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        return new ConfigurationBuilder()
            .AddJsonFile($"{rootPath}/appsettings.json")
            .AddEnvironmentVariables()
            .Build()
            .GetValue<string>("MongoBinaryDirectory")
                ?? throw new FileLoadException("Could not find MongoBinaryDirectory in test appsettings");
    }
}
