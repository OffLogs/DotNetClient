using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OffLogs.Client.AspNetCore
{
    public class OffLogsLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<OffLogsLoggerConfiguration> _getCurrentConfig;

        public OffLogsLogger(
            string name,
            Func<OffLogsLoggerConfiguration> getCurrentConfig
        ) => (_name, _getCurrentConfig) = (name, getCurrentConfig);

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= _getCurrentConfig().MinLogLevel;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            OffLogsLoggerConfiguration config = _getCurrentConfig();
            Console.WriteLine($"     {_name} - {formatter(state, exception)}");
        }
    }
}
