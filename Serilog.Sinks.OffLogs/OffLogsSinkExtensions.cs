using System;
using Microsoft.Extensions.Configuration;
using OffLogs.Client;
using Serilog.Configuration;
using Serilog.Events;

namespace Serilog.Sinks.OffLogs
{
    public static class OffLogsSinkExtensions
    {
        /// <param name="loggerConfiguration">Configuration</param>
        /// <param name="apiToken">OffLogs api token</param>
        /// <param name="restrictedToMinimumLevel">
        /// The minimum level for events passed through the sink.
        /// </param>
        public static LoggerConfiguration OffLogs(
            this LoggerSinkConfiguration loggerConfiguration,
            string apiToken,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum
        )
        {
            return loggerConfiguration.Sink(new OffLogsSink(apiToken, restrictedToMinimumLevel));
        }

        /// <param name="loggerConfiguration">Configuration</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="restrictedToMinimumLevel">
        /// The minimum level for events passed through the sink.
        /// </param>
        public static LoggerConfiguration OffLogs(
            this LoggerSinkConfiguration loggerConfiguration,
            IConfiguration configuration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum
        )
        {
            var apiToken = configuration["OffLogs:ApiToken"];
            return loggerConfiguration.Sink(new OffLogsSink(apiToken, restrictedToMinimumLevel));
        }
    }
}