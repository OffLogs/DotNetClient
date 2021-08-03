using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OffLogs.Client.AspNetCore
{
    public class OffLogsLoggerConfiguration
    {
        public string ApiToken { get; set; }

        public LogLevel MinLogLevel { get; set; } = LogLevel.Warning;
    }
}
