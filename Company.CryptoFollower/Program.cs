using Company.CryptoFollower.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddScoped<IGetCryptoCurrencyInfoService, GetCryptoCurrencyInfoService>();
        s.AddHttpClient();
    })
    .Build();

host.Run();