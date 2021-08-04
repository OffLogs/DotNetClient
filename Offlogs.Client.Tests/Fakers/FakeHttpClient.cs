using Microsoft.Extensions.Logging;
using OffLogs.Client;
using OffLogs.Client.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offlogs.Client.Tests.Fakers
{
    public class FakeHttpClient : IHttpClient
    {
        public List<List<LogDto>> SentBanches = new List<List<LogDto>>();

        public void Dispose() 
        {
            SentBanches.Clear();
        }

        public Task SendLog(LogLevel level, string message, IDictionary<string, string> properties)
        {
            // AspNetCore client does not use this
            throw new NotImplementedException();
        }

        public Task SendLogAsync(LogLevel level, string message, ICollection<string> traces = null, IDictionary<string, string> properties = null)
        {
            // AspNetCore client does not use this
            throw new NotImplementedException();
        }

        public Task SendLogsAsync(ICollection<LogDto> logs)
        {
            if (logs.Count > 0)
                SentBanches.Add(logs.ToList());
            return Task.CompletedTask;
        }

        public void SetApiToken(string apiToken) {}
    }
}
