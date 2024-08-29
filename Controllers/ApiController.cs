using Microsoft.AspNetCore.Mvc;
using PF2SrdApi.Models;
using PF2SrdApi.Services;

namespace PF2SrdApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    private readonly ApiService apiService;

    public ApiController(ApiService apiService)
    {
        this.apiService = apiService;
    }

    [HttpGet("alignments")]
    public async Task<ResultsWithCount<Alignment>> GetAlignments()
    {
        return await this.apiService.GetAsync<Alignment>();
    }

    [HttpGet("alignments/{index}")]
    public async Task<Alignment> GetAlignment(string index)
    {
        var alignment = await this.apiService.GetAsync<Alignment>(index);

        if (alignment is null)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return alignment;
    }

    [HttpGet("monsters")]
    public async Task<ResultsWithCount<MonsterMinimal>> GetMonsters(int? level = null)
    {
        return await this.apiService.GetMonsters(level);
    }

    [HttpGet("monsters/{index}")]
    public async Task<MonsterMinimal> GetMonster(string index)
    {
        var monster = await this.apiService.GetAsync<MonsterMinimal>(index);

        if (monster is null)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return monster;
    }
}
