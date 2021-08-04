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
        private readonly OffLogsHttpClient Client;

        public OfflogsHttpClientTests()
        {
            Client = new OffLogsHttpClient(ApiToken);
        }

        #region Without errors
        [Theory]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Trace)]
        [InlineData(LogLevel.Warning)]
        [InlineData(LogLevel.None)]
        public async Task ShouldSendSimpleLogAsync(LogLevel logLevel)
        {
            await Client.SendLogAsync(logLevel, "Some Debug message");
        }

        [Theory]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Trace)]
        [InlineData(LogLevel.Warning)]
        [InlineData(LogLevel.None)]
        public async Task ShouldSendLogWithPropertiesAsync(LogLevel logLevel)
        {
            var properties = new Dictionary<string, string>();
            properties.Add("1", "2");
            properties.Add("3", "4");
            await Client.SendLogAsync(logLevel, "Some Debug message", null, properties);
        }

        [Theory]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Trace)]
        [InlineData(LogLevel.Warning)]
        [InlineData(LogLevel.None)]
        public async Task ShouldSendLogWithTracesAsync(LogLevel logLevel)
        {
            var traces = new List<string>();
            traces.Add("trace 1");
            traces.Add("trace 2");
            traces.Add("trace 3");
            await Client.SendLogAsync(logLevel, "Some Debug message", traces);
        }
        #endregion

        #region With errors
        [Fact]
        public async Task ShouldThowErrorIfMessageIsEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => {
                await Client.SendLogAsync(LogLevel.Debug, "");
            });
        }
        #endregion

        #region Configuration tests
        [Theory]
        [InlineData(LogLevel.Error)]
        [InlineData(LogLevel.Debug)]
        [InlineData(LogLevel.Information)]
        [InlineData(LogLevel.Trace)]
        [InlineData(LogLevel.Warning)]
        [InlineData(LogLevel.None)]
        public async Task ShouldSetApiTokenAndSendLogAsync(LogLevel logLevel)
        {
            var client = new OffLogsHttpClient();
            client.SetApiToken(ApiToken);
            await client.SendLogAsync(logLevel, "Some Debug message");
        }
        #endregion
    }
}
