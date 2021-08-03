using Microsoft.Extensions.Logging;
using OffLogs.Client.Dto;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OffLogs.Client.AspNetCore.Sender
{
    public class OffLogsLogSender : IOffLogsLogSender
    {
        private const int BatchSize = 50;
        private const double SendingInteval = 5000;

        private readonly IHttpClient _httpClient;
        private readonly ConcurrentQueue<LogDto> _queue;
        private readonly Timer _timer;

        public OffLogsLogSender()
        {
            _queue = new ConcurrentQueue<LogDto>();
            _httpClient = new HttpClient();
            _timer = new Timer();
            _timer.Elapsed += SendingTimer_Elapsed;
            _timer.Interval = SendingInteval;
            _timer.Start();
        }

        private void SendingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            SendLogsBanchAsync().Wait();
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

        public Task SendAsync(LogLevel level, string message)
        {
            var logDto = CreateDto(level, message);
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

        private async Task SendLogsBanchAsync()
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
                await SendLogsBanchAsync();
            }
        }

        private LogDto CreateDto(LogLevel level, string message)
        {
            var dto = new LogDto(
                level,
                message,
                DateTime.Now
            );
            return dto;
        }
    }
}
