using CryptoFollower.API;
using CryptoFollower.Core;
using CryptoFollower.Core.Settings;
using CryptoFollower.Functions.Services;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace CryptoFollower.Functions;

public static class DependencyConfigurationFunction
{
    public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddAzureClients(b =>
        {
            b.AddTableServiceClient(context.Configuration["AzureWebJobsStorage"]);
        });
        services.AddHttpClient();
        
        services.Configure<AppSettings>(context.Configuration);
        
        services.AddScoped<IUserNotificationService, UserNotificationService>();
        services.AddScoped<IAlertTriggerService, AlertTriggerService>();
        
        DependencyConfigurationApi.ConfigureServices(services);
        DependencyConfigurationCore.ConfigureServices(services);
    }

    public static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder builder)
    {
        if(bool.Parse(context.Configuration["UseAppInsightsLogging"] ?? "false"))
            builder.AddApplicationInsights(
                configureTelemetryConfiguration: (config) => config.ConnectionString = context.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"],
                configureApplicationInsightsLoggerOptions: (options) =>
                { }
            );
    
        // Capture all log-level entries 
        builder.AddFilter<ApplicationInsightsLoggerProvider>(
            "CryptoFollowerFunctions", LogLevel.Trace);
    }
}