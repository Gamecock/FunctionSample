using Azure.Identity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s => 
        {
        // https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide#start-up-and-configuration
            s.AddApplicationInsightsTelemetryWorkerService();
            s.ConfigureFunctionsApplicationInsights();
            })
        .ConfigureLogging(logging =>
        {
            logging.Services.Configure<LoggerFilterOptions>(options =>
            {
                LoggerFilterRule? defaultRule = options.Rules.FirstOrDefault(rule => rule.ProviderName
                    == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");
                if (defaultRule is not null)
                {
                    options.Rules.Remove(defaultRule);
                }
            });
        })
    .Build();

host.Run();
