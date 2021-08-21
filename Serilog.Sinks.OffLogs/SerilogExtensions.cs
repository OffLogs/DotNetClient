using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace Serilog.Sinks.OffLogs
{
    public static class SerilogExtensions
    {
        public static LogLevel GetDotNetLogLevel(this LogEventLevel logLevel)
        {
            if (logLevel == LogEventLevel.Verbose)
                return LogLevel.Trace;
            if (logLevel == LogEventLevel.Debug)
                return LogLevel.Debug;
            if (logLevel == LogEventLevel.Information)
                return LogLevel.Information;
            if (logLevel == LogEventLevel.Warning)
                return LogLevel.Warning;
            if (logLevel == LogEventLevel.Error)
                return LogLevel.Error;
            if (logLevel == LogEventLevel.Fatal)
                return LogLevel.Critical;
            
            return LogLevel.None;
        }
    }
}