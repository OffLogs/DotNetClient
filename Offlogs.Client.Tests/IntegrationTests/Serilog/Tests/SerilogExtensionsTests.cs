using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Xunit;
using Serilog.Sinks.OffLogs;

namespace Offlogs.Client.Tests.IntegrationTests.Serilog.Tests
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