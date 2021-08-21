using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Offlogs.Client.Tests.IntegrationTests.Serilog.Integration.Tests
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
        }
    }
}
