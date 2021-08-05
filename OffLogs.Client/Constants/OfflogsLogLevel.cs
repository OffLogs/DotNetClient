using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OffLogs.Client.Constants
{
    public class OffLogsLogLevel
    {
        public static readonly OffLogsLogLevel Error = new OffLogsLogLevel("E", "Error");
        public static readonly OffLogsLogLevel Warning = new OffLogsLogLevel("W", "Warning");
        public static readonly OffLogsLogLevel Fatal = new OffLogsLogLevel("F", "Fatal");
        public static readonly OffLogsLogLevel Information = new OffLogsLogLevel("I", "Information");
        public static readonly OffLogsLogLevel Debug = new OffLogsLogLevel("D", "Debug");

        protected readonly string _Name;
        protected readonly string _Value;

        public OffLogsLogLevel() { }

        private OffLogsLogLevel(string value, string name) 
        {
            _Value = value;
            _Name = name;
        }

        public override string ToString()
        {
            return _Name;
        }

        public string GetValue()
        {
            return _Value;
        }

        public static OffLogsLogLevel GetFromLogLevel(LogLevel level)
        {
            if (level == LogLevel.Error)
                return Error;
            if (level == LogLevel.Critical)
                return Fatal;
            if (level == LogLevel.Debug || level == LogLevel.Trace)
                return Debug;
            if (level == LogLevel.Warning)
                return Warning;
            if (level == LogLevel.Information)
                return Information;
            return Debug;
        }
    }
}
