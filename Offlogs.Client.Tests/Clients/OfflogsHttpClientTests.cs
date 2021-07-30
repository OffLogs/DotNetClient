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

        [Theory]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Trace)]
        [InlineData(LogLevel.Warning)]
        [InlineData(LogLevel.None)]
        public async Task ShouldSendSimpleLog(LogLevel logLevel)
        {
            await Client.SendLogAsync(logLevel, "Some Debug message");
        }
    }
}
