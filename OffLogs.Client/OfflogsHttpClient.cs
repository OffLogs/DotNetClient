using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace OffLogs.Client
{
    public class OfflogsHttpClient
    {
        private static readonly HttpClient _client = new HttpClient();

        public void SendLog(
            LogLevel level,
            string message,
            IDictionary<string, string> properties
        )
        {
            SendLog(level, message, null, properties);
        }

        public void SendLog(
            LogLevel level, 
            string message,
            ICollection<string> traces = null,
            IDictionary<string, string> properties = null
        )
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
        }
    }
}
