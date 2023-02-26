using Company.CryptoFollower.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddScoped<IGetCoinInfoService, GetCoinInfoService>();
        s.AddHttpClient();
    })
    // .ConfigureLogging((context, builder) =>
    // {
    //     builder.AddApplicationInsights(
    //         configureTelemetryConfiguration: (config) => config.ConnectionString = context.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"],
    //         configureApplicationInsightsLoggerOptions: (options) => { }
    //     );
    //
    //     // Capture all log-level entries from Startup
    //     builder.AddFilter<ApplicationInsightsLoggerProvider>(
    //         "CryptoNotifier", LogLevel.Trace);
    // })
    .Build();

host.Run();