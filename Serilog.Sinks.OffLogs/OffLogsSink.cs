using System;
using Microsoft.Extensions.Logging;
using OffLogs.Client;
using OffLogs.Client.Senders;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.OffLogs
{
    public class OffLogsSink : ILogEventSink, IDisposable
    {
        private readonly string _apiToken;
        private readonly LogEventLevel _restrictedToMinimumLevel;
        private readonly IOffLogsHttpClient _offLogsHttpClient;
        private readonly IOffLogsLogSender _offLogsLogSender;
        
        /// <param name="apiToken">OffLogs api token</param>
        /// <param name="restrictedToMinimumLevel">
        /// The minimum level for events passed through the sink.
        /// </param>
        public OffLogsSink(
            string apiToken,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum
        )
        {
            _apiToken = apiToken;
            _restrictedToMinimumLevel = restrictedToMinimumLevel;
            _offLogsHttpClient = new OffLogsHttpClient();
            _offLogsHttpClient.SetApiToken(apiToken);
            _offLogsLogSender = new OffLogsLogSender(_offLogsHttpClient);
        }
 
        public void Emit(LogEvent logEvent)
        {
            // _offLogsLogSender.SendAsync(
            //     
            // );
        }

        public void Dispose()
        {
            _offLogsHttpClient?.Dispose();
            _offLogsLogSender?.Dispose();
        }
    }
}