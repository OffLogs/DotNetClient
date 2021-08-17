using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Logging;
using OffLogs.Client.Dto;

namespace OffLogs.Client.Senders
{
    public class OffLogsLogSender : IOffLogsLogSender
    {
        private const int BatchSize = 50;
        private const double SendingInteval = 5000;

        private readonly IOffLogsHttpClient _httpClient;
        private readonly ConcurrentQueue<LogDto> _queue;
        private readonly Timer _timer;

        public OffLogsLogSender(IOffLogsHttpClient httpClient)
        {
            _queue = new ConcurrentQueue<LogDto>();
            _httpClient = httpClient;
            _timer = new Timer();
            _timer.Elapsed += SendingTimer_Elapsed;
            _timer.Interval = SendingInteval;
            _timer.Start();
        }

        private void SendingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            SendLogsBunchAsync().Wait();
            _timer.Start();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public void SetApiToken(string apiToken)
        {
            _httpClient.SetApiToken(apiToken);
        }

        public Task SendAsync(
            LogLevel level, 
            string message, 
            IDictionary<string, string> properties = null,
            DateTime? timestamp = null
        )
        {
            var logDto = CreateDto(level, message, timestamp);
            if (properties != null)
            {
                logDto.AddProperties(properties);
            }
            _queue.Enqueue(logDto);
            return Task.CompletedTask;
        }

        public Task SendAsync(LogLevel level, Exception exception)
        {
            var logDto = CreateDto(level, exception.Message);
            if (exception.StackTrace != null)
            {
                logDto.AddTraces(
                    exception.StackTrace.Split("\n")
                );
            }
            if (exception.Data != null)
            {
                foreach (DictionaryEntry keyValuePair in exception.Data)
                {
                    logDto.AddProperty($"{keyValuePair.Key}", $"{keyValuePair.Value}");
                }
            }
            _queue.Enqueue(logDto);
            return Task.CompletedTask;
        }

        private async Task SendLogsBunchAsync()
        {
            var logsToSend = new List<LogDto>();
            while (true)
            {
                var isExists = _queue.TryDequeue(out var logDto);
                if (!isExists || logsToSend.Count >= BatchSize)
                {
                    break;
                }
                if (isExists)
                {
                    logsToSend.Add(logDto);
                }
            }
            try
            {
                await _httpClient.SendLogsAsync(logsToSend);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"OffLogs logger error: {exception.Message} - {exception.StackTrace}");
            }

            // logs can be produced faster than 50 times per second,
            // so we continue the process until the list of logs runs out
            if (!_queue.IsEmpty)
            {
                await SendLogsBunchAsync();
            }
        }

        private LogDto CreateDto(LogLevel level, string message, DateTime? timstamp = null)
        {
            var dto = new LogDto(
                level,
                message,
                timstamp ?? DateTime.Now
            );
            return dto;
        }
    }
}
