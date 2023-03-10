using Company.CryptoFollower.Services;
using Company.CryptoFollower.Settings;
using Company.CryptoFollower.Storage;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, s) =>
    {
        s.AddAzureClients(b =>
        {
            b.AddTableServiceClient(context.Configuration["AzureWebJobsStorage"]);
        });
        s.AddHttpClient();
        s.Configure<AppSettings>(context.Configuration);
        s.AddScoped<IGetCoinInfoService, GetCoinInfoService>();
        s.AddScoped<ITelegramNotifierService, TelegramNotifierService>();
        s.AddScoped<IMailNotifierService, MailNotifierService>();
        s.AddScoped<INotificationUserService, NotificationUserService>();
        s.AddScoped<IAzureTableRepository, AzureTableRepository>();
        s.AddScoped<IAlertTriggerService, AlertTriggerService>();
    })
    .ConfigureLogging((context, builder) =>
    {
        if(bool.Parse(context.Configuration["UseAppInsightsLogging"] ?? "false"))
            builder.AddApplicationInsights(
                configureTelemetryConfiguration: (config) => config.ConnectionString = context.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"],
                configureApplicationInsightsLoggerOptions: (options) =>
                { }
            );
    
        // Capture all log-level entries from Startup
        builder.AddFilter<ApplicationInsightsLoggerProvider>(
            "CryptoFollower", LogLevel.Trace);
    })
    .Build();

host.Run();