using Microsoft.Extensions.Logging;
using OffLogs.Client.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Offlogs.Client.Tests.Constants
{
    public class OffLogsLogLevelTests
    {
        [Theory]
        [InlineData(LogLevel.Debug, "D")]
        [InlineData(LogLevel.Trace, "D")]
        [InlineData(LogLevel.Warning, "W")]
        [InlineData(LogLevel.Critical, "F")]
        [InlineData(LogLevel.Information, "I")]
        [InlineData(LogLevel.Error, "E")]
        [InlineData(LogLevel.None, "D")]
        public void ShouldReturnOfflogLogTypeName(LogLevel logLevel, string offLogLogName)
        {
            Assert.Equal(
                OffLogsLogLevel.GetFromLogLevel(logLevel).GetValue(),
                offLogLogName
            );
        }
    }
}
