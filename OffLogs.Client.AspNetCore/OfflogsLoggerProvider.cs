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
        private OffLogsLoggerConfiguration _offLogsConfig;
        private readonly ConcurrentDictionary<string, OffLogsLogger> _loggers = new ConcurrentDictionary<string, OffLogsLogger>();
        private readonly IOffLogsLogSender _offLogsLogSender;
        private readonly IConfiguration _configuration;

        private string ApiToken
        {
            get {
                if (!string.IsNullOrEmpty(_offLogsConfig.ApiToken))
                {
                    return _offLogsConfig.ApiToken;
                }
                var apiTokenFromAppSettings = _configuration.GetValue<string>("OffLogs:ApiToken");
                if (!string.IsNullOrEmpty(apiTokenFromAppSettings))
                {
                    return apiTokenFromAppSettings;
                }
                throw new ArgumentNullException("OffLogs API token not found!");
            }
        }

        public OfflogsLoggerProvider(
            IOptionsMonitor<OffLogsLoggerConfiguration> config,
            IOffLogsLogSender offLogsLogSender,
            IConfiguration configuration
        )
        {
            _configuration = configuration;
            _offLogsConfig = config.CurrentValue;
            _offLogsLogSender = offLogsLogSender;
            _offLogsLogSender.SetApiToken(ApiToken);
            _onChangeToken = config.OnChange(updatedConfig => {
                _offLogsConfig = updatedConfig;
                _offLogsLogSender.SetApiToken(_offLogsConfig.ApiToken);
            });
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => {
                return new OffLogsLogger(name, GetCurrentConfig, GetLogSender);
            });
        }

        private OffLogsLoggerConfiguration GetCurrentConfig() => _offLogsConfig;

        private IOffLogsLogSender GetLogSender() => _offLogsLogSender;

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}
