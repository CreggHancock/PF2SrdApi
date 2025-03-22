using MongoDB.Driver;
using PF2SrdApi.Models.Spells;

namespace PF2SrdApi.Models;

public class Query
{
    [UseProjection]
    public IExecutable<Spell> GetSpells(
    [Service] IMongoCollection<Spell> collection)
    => collection.AsExecutable();

    [UseFirstOrDefault]
    public IExecutable<Spell> GetSpellById(
        [Service] IMongoCollection<Spell> collection,
        [ID] string id)
        => collection.Find(x => x.Id == id).AsExecutable();
}
