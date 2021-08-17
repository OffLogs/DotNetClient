using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Offlogs.Client.Tests.IntegrationTests.Serilog.Tests
{
    public class SerilogSinkTests : SerilogBaseTest
    {
        public SerilogSinkTests(AspNetCore3WebApplicationFactory factory) : base(factory) {}

        [Theory]
        [InlineData("/log/info", "Info", LogLevel.Information)]
        [InlineData("/log/debug", "Debug", LogLevel.Debug)]
        [InlineData("/log/warning", "Warning", LogLevel.Warning)]
        [InlineData("/log/error", "Error", LogLevel.Error)]
        [InlineData("/log/trace", "Trace", LogLevel.Trace)]
        [InlineData("/log/critical", "Critical", LogLevel.Critical)]
        public async Task ShouldSendSimpleInfoLog(string url, string messagePart, LogLevel level)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Wait while logs will processed
            Thread.Sleep(6000);

            //Assert.True(_offlogsHttpClient.SentBanches.Count > 0);
            //foreach (var branch in _offlogsHttpClient.SentBanches)
            //{
            //    Assert.Contains(branch, l => l.OriginalLevel == level);
            //    Assert.Contains(branch, l => l.Message.Contains(messagePart));
            //}
        }
    }
}
