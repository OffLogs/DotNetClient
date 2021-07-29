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
        [JsonProperty]
        public string Level { get; set; }

        [JsonProperty]
        public DateTime Timestamp { get; set; }

        [JsonProperty]
        public string Message { get; set; }

        [JsonProperty]
        public List<string> Traces { get; } = new List<string>();

        [JsonProperty]
        public Dictionary<string, string> Properties { get; } = new Dictionary<string, string>();

        public LogDto(LogLevel level, string message, DateTime? timestamp = null)
        {
            Level = OfflogsLogLevel.GetFromLogLevel(level).GetValue();
            Message = message;
            Timestamp = timestamp ?? DateTime.Now;

            if (string.IsNullOrEmpty(Message))
                throw new ArgumentNullException(nameof(message));
        }

        public void AddProperty(string key, string value)
        {
            Properties.Add(key, value);
        }

        public void AddProperties(IDictionary<string, string> properties)
        {
            foreach (var keyValuePair in properties)
                AddProperty(keyValuePair.Key, keyValuePair.Value);
        }

        public void AddTrace(string trace)
        {
            Traces.Add(trace);
        }

        public void AddTraces(ICollection<string> traces)
        {
            foreach (var trace in traces)
                AddTrace(trace);
        }
    }
}
