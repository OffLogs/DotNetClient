using Microsoft.Extensions.Logging;
using OffLogs.Client.Constants;
using OffLogs.Client.Dto;
using System;
using System.Collections.Generic;
using Xunit;

namespace Offlogs.Client.Tests.Dto
{
    public class LogDtoTesLogsListDtoTeststs
    {
        [Fact]
        public void ShouldCreateDto()
        {
            var logDto1 = new LogDto(LogLevel.Warning, "Message 1");
            var logDto2 = new LogDto(LogLevel.Debug, "Message 2");
            var expectedList = new List<LogDto>() { logDto1, logDto2 };

            var dto = new LogsListDto(expectedList);

            Assert.Equal(expectedList, dto.Logs);
        }

        [Fact]
        public void ShouldThrowExceptionIfTooManyProperties()
        {
            var logs = new List<LogDto>();
            for (var i = 0; i <= 100; i++)
            {
                logs.Add(new LogDto(LogLevel.Warning, $"{i}"));
            }

            Assert.Throws<Exception>(() => {
                var dto = new LogsListDto(logs);
            });
        }
    }
}
