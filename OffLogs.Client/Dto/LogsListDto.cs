using Newtonsoft.Json;
using OffLogs.Client.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OffLogs.Client.Dto
{
    internal class LogsListDto
    {
        [JsonProperty("logs")]
        public List<LogDto> Logs { get; }

        private readonly int _maxLogs = 99;

        public LogsListDto(ICollection<LogDto> logs)
        {
            Logs = logs.ToList();
            IsSizeCorrect();
        }

        public void AddLog(LogDto log)
        {
            Logs.Add(log);
            IsSizeCorrect();
        }

        public void IsSizeCorrect()
        {
            if (Logs.Count >= _maxLogs)
                throw new Exception($"Too many logs in one request! Max: {_maxLogs}");
        }
    }
}
