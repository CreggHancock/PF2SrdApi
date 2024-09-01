using AutoMapper;

namespace PF2SrdApi.Tests;

public class AutoMapperTests
{
    [Fact]
    public void AutoMapperProfile_ConfigurationIsValid()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });

        config.AssertConfigurationIsValid();
    }
}