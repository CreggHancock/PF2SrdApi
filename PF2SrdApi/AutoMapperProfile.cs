using AutoMapper;
using PF2SrdApi.Models;

namespace PF2SrdApi;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        this.CreateMap<Monster, MonsterMinimal>();
    }
}
