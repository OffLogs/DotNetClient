using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog.Sinks.OffLogs;
using Xunit;

namespace Offlogs.Client.Tests.IntegrationTests.Serilog.Unit.Tests
{
    public class SerilogExtensionsTests
    {
        [Theory]
        [InlineData(LogEventLevel.Verbose, LogLevel.Trace)]
        [InlineData(LogEventLevel.Information, LogLevel.Information)]
        [InlineData(LogEventLevel.Debug, LogLevel.Debug)]
        [InlineData(LogEventLevel.Warning, LogLevel.Warning)]
        [InlineData(LogEventLevel.Error, LogLevel.Error)]
        [InlineData(LogEventLevel.Fatal, LogLevel.Critical)]
        public void ShouldConvertSerilogLogLevel(LogEventLevel serilogLevel, LogLevel level)
        {
            Assert.Equal(level, serilogLevel.GetDotNetLogLevel());
        }
    }
}