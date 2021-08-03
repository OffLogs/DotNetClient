using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        public OfflogsLoggerProvider(
            IOptionsMonitor<OffLogsLoggerConfiguration> config
        )
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new OffLogsLogger(name, GetCurrentConfig));
        }

        private OffLogsLoggerConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}
