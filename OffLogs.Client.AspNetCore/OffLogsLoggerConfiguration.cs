using Microsoft.Extensions.Logging;

namespace OffLogs.Client.AspNetCore
{
    public class OffLogsLoggerConfiguration
    {
        public string ApiToken { get; set; }

        public LogLevel MinLogLevel { get; set; } = LogLevel.Warning;
    }
}
