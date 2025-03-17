using Microsoft.AspNetCore.Mvc;
using PF2SrdApi.Models;
using PF2SrdApi.Models.Spells;
using PF2SrdApi.Services;
using System;

namespace PF2SrdApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiController(ApiService apiService)
    : ControllerBase
{
    [HttpGet("alignments")]
    public async Task<ResultsWithCount<Alignment>> GetAlignments()
    {
        return await apiService.Get<Alignment>();
    }

    [HttpGet("alignments/{id}")]
    public async Task<Alignment> GetAlignment(string id)
    {
        var alignment = await apiService.Get<Alignment>(id);
        return alignment ?? throw new ArgumentOutOfRangeException(nameof(id));
    }

    [HttpGet("monsters")]
    public async Task<ResultsWithCount<MonsterMinimal>> GetMonsters(int? level = null)
    {
        return await apiService.GetMonsters(level);
    }

    [HttpGet("monsters/{id}")]
    public async Task<MonsterMinimal> GetMonster(string id)
    {
        var monster = await apiService.Get<MonsterMinimal>(id);
        return monster ?? throw new ArgumentOutOfRangeException(nameof(id));
    }

    [HttpGet("spells")]
    public async Task<ResultsWithCount<Spell>> GetSpells(int? level = null)
    {
        return await apiService.GetSpells(level);
    }

    [HttpGet("spells/{id}")]
    public async Task<Spell> GetSpell(string id)
    {
        var spell = await apiService.Get<Spell>(id);
        return spell ?? throw new ArgumentOutOfRangeException(nameof(id));
    }
}
