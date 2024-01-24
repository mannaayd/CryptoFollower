using CryptoFollower.Functions;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(DependencyConfigurationFunction.ConfigureServices)
    .ConfigureLogging(DependencyConfigurationFunction.ConfigureLogging)
    .Build();

host.Run();