using System;
using Serilog.Events;

namespace Owin.Study.Legacy.Logging
{
    public class LogEntry
    {
        public string Level { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Message { get; set; }
    }
}