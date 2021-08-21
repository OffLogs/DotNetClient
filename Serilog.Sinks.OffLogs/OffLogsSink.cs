using System.Linq;
using OffLogs.Client;
using OffLogs.Client.Senders;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.OffLogs
{
    public class OffLogsSink : ILogEventSink
    {
        private readonly string _apiToken;
        private readonly LogEventLevel _restrictedToMinimumLevel;
        private readonly IOffLogsHttpClient _offLogsHttpClient;
        private readonly IOffLogsLogSender _offLogsLogSender;

        /// <param name="apiToken">OffLogs api token</param>
        /// <param name="restrictedToMinimumLevel">
        /// The minimum level for events passed through the sink.
        /// </param>
        /// <param name="httpClient">Custom realization of http client</param>
        public OffLogsSink(
            string apiToken,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IOffLogsHttpClient httpClient = null
        )
        {
            _apiToken = apiToken;
            _restrictedToMinimumLevel = restrictedToMinimumLevel;
            _offLogsHttpClient = httpClient ?? new OffLogsHttpClient();
            _offLogsHttpClient.SetApiToken(apiToken);
            _offLogsLogSender = new OffLogsLogSender(_offLogsHttpClient);
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Exception != null)
            {
                _offLogsLogSender.SendAsync(
                    logEvent.Level.GetDotNetLogLevel(),
                    logEvent.Exception
                );
                return;
            }
            var properties = logEvent.Properties
                .ToDictionary(k => k.Key, v => v.Value.ToString());
            var message = logEvent.RenderMessage();
            _offLogsLogSender.SendAsync(
                logEvent.Level.GetDotNetLogLevel(),
                message,
                properties
            );
        }

        ~OffLogsSink()
        {
            _offLogsHttpClient?.Dispose();
            _offLogsLogSender?.Dispose();
        }
    }
}