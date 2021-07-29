using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OffLogs.Client.Constants
{
    internal class OfflogsLogLevel
    {
        public static readonly OfflogsLogLevel Error = new OfflogsLogLevel("E", "Error");
        public static readonly OfflogsLogLevel Warning = new OfflogsLogLevel("W", "Warning");
        public static readonly OfflogsLogLevel Fatal = new OfflogsLogLevel("F", "Fatal");
        public static readonly OfflogsLogLevel Information = new OfflogsLogLevel("I", "Information");
        public static readonly OfflogsLogLevel Debug = new OfflogsLogLevel("D", "Debug");

        protected readonly string _Name;
        protected readonly string _Value;

        public OfflogsLogLevel() { }

        private OfflogsLogLevel(string value, string name) { }

        public override string ToString()
        {
            return _Name;
        }

        public string GetValue()
        {
            return _Value;
        }

        public static OfflogsLogLevel GetFromLogLevel(LogLevel level)
        {
            if (level == LogLevel.Debug || level == LogLevel.Trace)
                return Debug;
            if (level == LogLevel.Warning)
                return Warning;
            if (level == LogLevel.Information)
                return Information;
            return null;
        }
    }
}
