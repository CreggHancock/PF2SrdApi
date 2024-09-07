using AutoMapper;
using EphemeralMongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PF2SrdApi.Models;
using PF2SrdApi.Services;
using Shouldly;

namespace PF2SrdApi.Tests.Services;

public class ApiServiceTests : TestBase
{
    [Fact]
    public async Task GetMonsters_GetsMonsters()
    {
        Monster[] expectedMonsters =
        [
            new Monster
            {
                Index = "mon",
                Name = "ster",
                Url = "mon/ster",
                Id = string.Empty,
                Level = 1,
            },
            new Monster
            {
                Index = "mini",
                Name = "mal",
                Url = "mini/mal",
                Id = string.Empty,
                Level = 2,
            },
        ];

        using var runner = this.CreateMongoRunner();
        var repository = await this.CreateApiRepository(runner, Monster.TableName, expectedMonsters);
        var apiService = new ApiService(repository, GetTestMapper());

        var monsters = await apiService.GetMonsters();

        monsters.Count.ShouldBe(2);
        monsters.Results.ShouldBe(expectedMonsters.Select(m => new MonsterMinimal
            {
                Index = m.Index,
                Name = m.Name,
                Url = m.Url,
                Id = m.Id,
            }));
    }

    [Fact]
    public async Task GetMonsters_WithLevelFilter_GetsFilteredMonsters()
    {
        MonsterMinimal expectedMonster = new ()
        {
            Index = "mon",
            Name = "ster",
            Url = "mon/ster",
            Id = string.Empty,
        };

        Monster[] allMonsters =
        [
            new Monster
            {
                Index = expectedMonster.Index,
                Name = expectedMonster.Name,
                Url = expectedMonster.Url,
                Id = expectedMonster.Id,
                Level = 1,
            },
            new Monster
            {
                Index = "mini",
                Name = "mal",
                Url = "mini/mal",
                Id = string.Empty,
                Level = 2,
            },
        ];

        using var runner = this.CreateMongoRunner();
        var repository = await this.CreateApiRepository(runner, Monster.TableName, allMonsters);
        var apiService = new ApiService(repository, GetTestMapper());

        var monsters = await apiService.GetMonsters(1);

        monsters.Count.ShouldBe(1);
        var monster = monsters.Results.Single();
        monster.Url.ShouldBe(expectedMonster.Url);
        monster.Name.ShouldBe(expectedMonster.Name);
        monster.Index.ShouldBe(expectedMonster.Index);
    }
}
