using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OffLogs.Client.AspNetCore.Sender;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace OffLogs.Client.AspNetCore
{
    public sealed class OfflogsLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable _onChangeToken;
        private OffLogsLoggerConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, OffLogsLogger> _loggers = new ConcurrentDictionary<string, OffLogsLogger>();
        private readonly IOffLogsLogSender _offLogsLogSender;

        public OfflogsLoggerProvider(
            IOptionsMonitor<OffLogsLoggerConfiguration> config,
            IOffLogsLogSender offLogsLogSender
        )
        {
            _currentConfig = config.CurrentValue;
            _offLogsLogSender = offLogsLogSender;
            _onChangeToken = config.OnChange(updatedConfig => {
                _currentConfig = updatedConfig;
                _offLogsLogSender.SetApiToken(_currentConfig.ApiToken);
            });
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => {
                return new OffLogsLogger(name, GetCurrentConfig, GetLogSender);
            });
        }

        private OffLogsLoggerConfiguration GetCurrentConfig() => _currentConfig;

        private IOffLogsLogSender GetLogSender() => _offLogsLogSender;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}
