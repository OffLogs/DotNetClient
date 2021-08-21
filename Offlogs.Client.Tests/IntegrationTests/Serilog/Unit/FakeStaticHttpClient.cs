using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OffLogs.Client;
using OffLogs.Client.Dto;

namespace Offlogs.Client.Tests.IntegrationTests.Serilog.Unit
{
    public class FakeStaticHttpClient : IOffLogsHttpClient
    {
        public static readonly List<List<LogDto>> SentBunches = new();
        public static readonly List<LogDto> SentLogs = new();

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
            {
                SentBunches.Add(logs.ToList());
                SentLogs.AddRange(logs);
            }

            return Task.CompletedTask;
        }

        public void SetApiToken(string apiToken) {}

        public static void Clear()
        {
            SentBunches.Clear();
            SentLogs.Clear();
        }

        public void Dispose() => Clear();
    }
}