using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OffLogs.Client.Senders
{
    public interface IOffLogsLogSender: IDisposable
    {
        void SetApiToken(string apiToken);
        Task SendAsync(LogLevel level, string message);
        Task SendAsync(LogLevel level, Exception exception);
    }
}
