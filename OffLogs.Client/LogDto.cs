using Newtonsoft.Json;
using OffLogs.Client.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OffLogs.Client
{
    internal class LogDto
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

        public LogDto(OfflogsLogLevel level, string message, DateTime? timestamp = null)
        {
            Level = level.GetValue();
            Message = message;
            Timestamp = timestamp ?? DateTime.Now;
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
