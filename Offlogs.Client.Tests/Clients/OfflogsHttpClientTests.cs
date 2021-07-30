using Microsoft.Extensions.Logging;
using OffLogs.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Offlogs.Client.Tests.Clients
{
    public class OfflogsHttpClientTests: BaseTest
    {
        private readonly OfflogsHttpClient Client;

        public OfflogsHttpClientTests()
        {
            Client = new OfflogsHttpClient(ApiToken);
        }

        [Fact]
        public async Task ShouldSendSimpleDebugLog()
        {
            await Client.SendLogAsync(LogLevel.Debug, "Some Debug message");
        }

        public async Task ShouldSendSimpleErrorLog()
        {
            await Client.SendLogAsync(LogLevel.Error, "Some Error message");
        }

        public async Task ShouldSendSimpleTraceLog()
        {
            await Client.SendLogAsync(LogLevel.Trace, "Some Trace message");
        }

        public async Task ShouldSendSimpleCriticalLog()
        {
            await Client.SendLogAsync(LogLevel.Critical, "Some Critical message");
        }

        public async Task ShouldSendSimpleWarningLog()
        {
            await Client.SendLogAsync(LogLevel.Warning, "Some Warning message");
        }

        public async Task ShouldSendSimpleInformationLog()
        {
            await Client.SendLogAsync(LogLevel.Information, "Some Information message");
        }
    }
}
