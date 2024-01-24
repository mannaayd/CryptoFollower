using CryptoFollower.Core.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoFollower.Core;

public static class DependencyConfigurationCore
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IAlertStorage, AlertStorage>();
        services.AddScoped<ICoinStorage, CoinStorage>();
    }
}