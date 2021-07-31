using Microsoft.Extensions.Logging;
using OffLogs.Client.Constants;
using OffLogs.Client.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OffLogs.Client
{
    public interface IOfflogsHttpClient: IDisposable
    {
        Task SendLog(
            LogLevel level,
            string message,
            IDictionary<string, string> properties
        );

        /// <summary>
        /// Send a log immediately as an asynchronous operation.
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="message">Log message</param>
        /// <param name="traces">List of the traces</param>
        /// <param name="properties">List of the properties</param>
        /// Exceptions:
        ///  T:System.Net.Http.HttpRequestException:
        ///    The HTTP response is unsuccessful.
        Task SendLogAsync(
            LogLevel level,
            string message,
            ICollection<string> traces = null,
            IDictionary<string, string> properties = null
        );

        /// <summary>
        /// Send a logs list immediately as an asynchronous operation.
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="message">Log message</param>
        /// <param name="traces">List of the traces</param>
        /// <param name="properties">List of the properties</param>
        /// Exceptions:
        ///  T:System.Net.Http.HttpRequestException:
        ///    The HTTP response is unsuccessful.
        Task SendLogsAsync(
            ICollection<LogDto> logs
        );
    }
}
