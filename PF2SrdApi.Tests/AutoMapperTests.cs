using AutoMapper;

namespace PF2SrdApi.Tests;

public class AutoMapperTests
{
    [Fact]
    public void Test1()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });

        config.AssertConfigurationIsValid();
    }
}