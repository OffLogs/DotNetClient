using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OffLogs.Client.AspNetCore.Sender
{
    public interface IOffLogsLogSender: IDisposable
    {
        void SetApiToken(string apiToken);
        Task SendAsync(LogLevel level, string message);
        Task SendAsync(LogLevel level, Exception exception);
    }
}
