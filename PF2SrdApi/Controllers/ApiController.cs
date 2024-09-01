using Microsoft.AspNetCore.Mvc;
using PF2SrdApi.Models;
using PF2SrdApi.Services;

namespace PF2SrdApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiController(ApiService apiService)
    : ControllerBase
{
    [HttpGet("alignments")]
    public async Task<ResultsWithCount<Alignment>> GetAlignments()
    {
        return await apiService.GetAsync<Alignment>();
    }

    [HttpGet("alignments/{index}")]
    public async Task<Alignment> GetAlignment(string index)
    {
        var alignment = await apiService.GetAsync<Alignment>(index);
        return alignment ?? throw new ArgumentOutOfRangeException(nameof(index));
    }

    [HttpGet("monsters")]
    public async Task<ResultsWithCount<MonsterMinimal>> GetMonsters(int? level = null)
    {
        return await apiService.GetMonsters(level);
    }

    [HttpGet("monsters/{index}")]
    public async Task<MonsterMinimal> GetMonster(string index)
    {
        var monster = await apiService.GetAsync<MonsterMinimal>(index);
        return monster ?? throw new ArgumentOutOfRangeException(nameof(index));
    }
}
