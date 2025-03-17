namespace PF2SrdApi.Services;

using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PF2SrdApi.Common;
using PF2SrdApi.Models;
using PF2SrdApi.Models.Spells;

public class ApiService(ApiRepository repository, IMapper mapper)
{
    public async Task<ResultsWithCount<MonsterMinimal>> GetMonsters(int? level = null)
    {
        var results = await repository.Get<Monster>()
            .Where(m => level == null || m.Level == level)
            .ToListAsync();

        return results.Select(mapper.Map<MonsterMinimal>).ToResultsWithCount();
    }

    public async Task<ResultsWithCount<Spell>> GetSpells(int? level = null)
    {
        var results = await repository.Get<Spell>()
            .Where(s => level == null || s.System.Level.Value == level)
            .ToListAsync();

        return results.Select(mapper.Map<Spell>).ToResultsWithCount();
    }

    public async Task<ResultsWithCount<T>> Get<T>()
        where T : EntityBase, IEntity
    {
        var results = await repository.Get<T>().ToListAsync();
        return results.ToResultsWithCount();
    }

    public async Task<T?> Get<T>(string id)
        where T : EntityBase, IEntity
    {
        return await repository.Get<T>().FirstOrDefaultAsync(x => x.Id == id);
    }
}
