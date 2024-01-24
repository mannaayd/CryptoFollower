using CryptoFollower.API.Mapper;
using CryptoFollower.API.Services;
using CryptoFollower.Core.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace CryptoFollower.API;

public static class DependencyConfigurationApi
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ICoinGeckoApiService, CoinGeckoApiService>();
        services.AddSingleton<ICoinGeckoMapperProvider, CoinGeckoMapperProvider>();
    }
}