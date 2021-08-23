using Microsoft.Extensions.Configuration;
using OffLogs.Client;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.OffLogs;

namespace Offlogs.Client.Tests.IntegrationTests.Serilog.Unit
{
    public static class OffLogsSinkExtensions
    {
        /// <param name="loggerConfiguration">Configuration</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="restrictedToMinimumLevel">
        /// The minimum level for events passed through the sink.
        /// </param>
        /// <param name="httpClient">Custom realization of http client</param>
        public static LoggerConfiguration OffLogs(
            this LoggerSinkConfiguration loggerConfiguration,
            IConfiguration configuration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IOffLogsHttpClient httpClient = null
        )
        {
            var apiToken = configuration["OffLogs:ApiToken"];
            return loggerConfiguration.Sink(new OffLogsSink(apiToken, restrictedToMinimumLevel, httpClient));
        }
    }
}