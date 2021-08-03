using Microsoft.Extensions.Logging;
using OffLogs.Client.AspNetCore.Sender;
using System;

namespace OffLogs.Client.AspNetCore
{
    public class OffLogsLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<OffLogsLoggerConfiguration> _getCurrentConfig;
        private readonly Func<IOffLogsLogSender> _getOffLogsSender;

        public OffLogsLogger(
            string name,
            Func<OffLogsLoggerConfiguration> getCurrentConfig,
            Func<IOffLogsLogSender> getOffLogsSender
        ) => (
            _name,
            _getCurrentConfig,
            _getOffLogsSender
        ) = (
            name,
            getCurrentConfig,
            getOffLogsSender
        );

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

            var config = _getCurrentConfig();
            var sender = _getOffLogsSender();
            if (exception == null)
            {
                sender.SendAsync(logLevel, $"{_name} - {formatter(state, exception)}").Wait();
            }
            else
            {
                sender.SendAsync(logLevel, exception).Wait();
            }
        }
    }
}
