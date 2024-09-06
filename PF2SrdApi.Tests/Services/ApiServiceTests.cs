using AutoMapper;
using EphemeralMongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PF2SrdApi.Models;
using PF2SrdApi.Services;
using Shouldly;

namespace PF2SrdApi.Tests.Services;

public class ApiServiceTests
{
    [Fact]
    public async Task GetMonsters_GetsMonsters()
    {
        MonsterMinimal[] expectedMonsters =
        [
            new MonsterMinimal
            {
                Index = "mon",
                Name = "ster",
                Url = "mon/ster",
                Id = string.Empty,
            },
            new MonsterMinimal
            {
                Index = "mini",
                Name = "mal",
                Url = "mini/mal",
                Id = string.Empty,
            },
        ];

        var mongoOptions = new MongoRunnerOptions
        {
            BinaryDirectory = GetMongoBinaryDirectory(),
        };
        using var runner = MongoRunner.Run(mongoOptions);
        var database = await GetDatabase(MonsterMinimal.TableName, runner);
        var collection = database.GetCollection<MonsterMinimal>(MonsterMinimal.TableName);
        await collection.InsertManyAsync(expectedMonsters);
        var repository = new ApiRepository(database);
        var apiService = new ApiService(repository, GetTestMapper());

        var monsters = await apiService.GetMonsters();

        monsters.Count.ShouldBe(2);
        monsters.Results.ShouldBe(expectedMonsters);
    }

    private static IMapper GetTestMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });

        return config.CreateMapper();
    }

    private static async Task<IMongoDatabase> GetDatabase(string tableName, IMongoRunner runner)
    {
        var database = new MongoClient(runner.ConnectionString).GetDatabase("default");
        await database.CreateCollectionAsync(tableName);
        return database;
    }

    private static string GetMongoBinaryDirectory()
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
