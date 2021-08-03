using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace OffLogs.Client.AspNetCore
{
    public static class OffLogsWebHostBuilderExtensions
    {
        public static ILoggingBuilder AddOffLogsLogger(
            this ILoggingBuilder builder
        )
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, OfflogsLoggerProvider>()
            );

            LoggerProviderOptions.RegisterProviderOptions<OffLogsLoggerConfiguration, OfflogsLoggerProvider>(
                builder.Services
            );

            return builder;
        }

        public static ILoggingBuilder AddColorConsoleLogger(
            this ILoggingBuilder builder,
            Action<OffLogsLoggerConfiguration> configure
        )
        {
            builder.AddOffLogsLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
