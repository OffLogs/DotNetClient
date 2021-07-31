using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OffLogs.Client.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OffLogs.Client.Dto
{
    public class LogDto
    {
        private const int _propertiesMaxCount = 99;

        [JsonProperty]
        public string Level { get; }

        [JsonProperty]
        public DateTime Timestamp { get; }

        [JsonProperty]
        public string Message { get; }

        [JsonProperty]
        public List<string> Traces { get; } = new List<string>();

        [JsonProperty]
        public Dictionary<string, string> Properties { get; } = new Dictionary<string, string>();

        public LogDto(LogLevel level, string message, DateTime? timestamp = null)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            Level = OffLogsLogLevel.GetFromLogLevel(level).GetValue();
            Message = message;
            Timestamp = timestamp ?? DateTime.Now;
        }

        public void AddProperty(string key, string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                return;

            Properties.Add(key, value);

            if (Properties.Count >= _propertiesMaxCount)
                throw new Exception($"Too many traces. Max: {_propertiesMaxCount}");
        }

        public void AddProperties(IDictionary<string, string> properties)
        {
            foreach (var keyValuePair in properties)
                AddProperty(keyValuePair.Key, keyValuePair.Value);
        }

        public void AddTrace(string trace)
        {
            if (string.IsNullOrEmpty(trace))
                return;

            Traces.Add(trace);

            if (Traces.Count >= _propertiesMaxCount)
                throw new Exception($"Too many traces. Max: {_propertiesMaxCount}");
        }

        public void AddTraces(ICollection<string> traces)
        {
            foreach (var trace in traces)
                AddTrace(trace);
        }
    }
}
