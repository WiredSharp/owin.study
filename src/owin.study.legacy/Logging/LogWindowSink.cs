using System;
using System.Linq;
using System.Threading.Tasks;
using Serilog.Events;
using System.Threading;
using Serilog.Configuration;
using Serilog;

namespace Owin.Study.Legacy.Logging
{
    internal class LogRepository : ILogRepository, IDisposable
    {
        private const int MAX_SIZE = 100;

        private readonly LogEntry[] _logs;

        private readonly ReaderWriterLockSlim _logslock;

        private int _index;

        public LogRepository()
        {
            _logslock = new ReaderWriterLockSlim();
            _logs = new LogEntry[MAX_SIZE];
            _index = 0;
        }

        public Task AddLogAsync(LogEntry logEntry)
        {
            return Task.Run(() => AddLog(logEntry));
        }

        private void AddLog(LogEntry logEntry)
        {
            bool locked = _logslock.TryEnterWriteLock(TimeSpan.FromSeconds(1));
            if (locked)
            {
                try
                {
                    _logs[_index] = logEntry;
                    _index = (_index + 1) % MAX_SIZE;
                }
                finally
                {
                    _logslock.ExitWriteLock();
                }
            }
        }

        public LogEntry[] GetLastLogs()
        {
            bool locked = _logslock.TryEnterReadLock(TimeSpan.FromSeconds(1));
            if (locked)
            {
                try
                {
                    return _logs.Where(l => l != null).OrderByDescending(l => l.TimeStamp).ToArray();
                }
                finally
                {
                    _logslock.ExitReadLock();
                }
            }
            else
            {
                return new LogEntry[0];
            }
        }

        public void Dispose()
        {
            _logslock.Dispose();
        }
    }

    internal class LogWindowSink : Serilog.Core.ILogEventSink
    {
        private ILogRepository _logRepository;

        public LogWindowSink(ILogRepository logRepository)
        {
            if (logRepository == null) throw new ArgumentNullException(nameof(logRepository));
            _logRepository = logRepository;
        }

        public void Emit(LogEvent logEvent)
        {
            _logRepository.AddLogAsync(ToLogEntry(logEvent));
        }

        private LogEntry ToLogEntry(LogEvent logEvent)
        {            
            return new LogEntry() { TimeStamp = logEvent.Timestamp, Level = logEvent.Level.ToString(), Message = logEvent.RenderMessage() };
        }
    }

    internal static class LoggerSinkConfigurationExtensions
    {
        public static LoggerConfiguration LogWindow(this LoggerSinkConfiguration config, ILogRepository logRepository)
        {
            return config.Sink(new LogWindowSink(logRepository));
        }
    }
}
