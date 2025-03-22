using MongoDB.Driver;

namespace PF2SrdApi.Models.Spells;

public class SpellNodeResolver
{
    public Task<Spell> ResolveAsync(
        [Service] IMongoCollection<Spell> collection,
        string id)
    {
        return collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}
