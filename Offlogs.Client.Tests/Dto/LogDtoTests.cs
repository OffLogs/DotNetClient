using Microsoft.Extensions.Logging;
using OffLogs.Client.Constants;
using OffLogs.Client.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace Offlogs.Client.Tests.Dto
{
    public class LogDtoTests
    {
        [Fact]
        public void ShouldCreateDto()
        {
            var expectedTime = DateTime.Now;
            var expectedMessage = "Some message";
            var expectedProperties = new Dictionary<string, string>();
            expectedProperties.Add("1", "2");
            expectedProperties.Add("3", "4");
            var expectedTraces = new List<string>();
            expectedTraces.Add("5");
            expectedTraces.Add("6");

            var dto = new LogDto(LogLevel.Warning, expectedMessage, expectedTime);
            dto.AddProperties(expectedProperties);
            dto.AddTraces(expectedTraces);

            Assert.Equal(OffLogsLogLevel.Warning.GetValue(), dto.Level);
            Assert.Equal(expectedMessage, dto.Message);
            Assert.Equal(expectedTime, dto.Timestamp);
            Assert.Equal(expectedProperties, dto.Properties);
            Assert.Equal(expectedTraces, dto.Traces);
        }

        [Fact]
        public void ShouldAddTimestampIfItNull()
        {
            var message = "Some message";

            var dto = new LogDto(LogLevel.Error, message);

            Assert.True(dto.Timestamp > DateTime.MinValue);
            Assert.True(dto.Timestamp <= DateTime.Now);
        }

        [Fact]
        public void ShouldAddDebugLevelIfItNone()
        {
            var dto = new LogDto(LogLevel.None, "Some message");
            Assert.Equal(OffLogsLogLevel.Debug.GetValue(), dto.Level);
        }

        [Fact]
        public void ShouldThrowExceptionIfMessageIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => {
                var dto = new LogDto(LogLevel.Error, null);
            });
        }

        [Fact]
        public void ShouldThrowExceptionIfTooManyTraces()
        {
            var traces = new HashSet<string>();
            for (var i = 0; i <= 100; i++)
            {
                traces.Add($"{i}");
            }

            Assert.Throws<Exception>(() => {
                var dto = new LogDto(LogLevel.Error, "Message");
                dto.AddTraces(traces);
            });
        }

        [Fact]
        public void ShouldThrowExceptionIfTooManyProperties()
        {
            var properties = new Dictionary<string, string>();
            for (var i = 0; i <= 100; i++)
            {
                properties.Add($"{i}", $"{i}");
            }

            Assert.Throws<Exception>(() => {
                var dto = new LogDto(LogLevel.Error, "Message");
                dto.AddProperties(properties);
            });
        }
    }
}
