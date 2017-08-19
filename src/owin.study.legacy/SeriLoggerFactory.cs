using System;
using System.Diagnostics;
using Microsoft.Owin.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Owin.Study.Legacy
{
    internal class SeriLoggerFactory : ILoggerFactory
    {
        private Serilog.ILogger logger;

        public SeriLoggerFactory(Serilog.ILogger logger)
        {
            this.logger = logger;
        }

        public Microsoft.Owin.Logging.ILogger Create(string name)
        {
            return new SeriLoggerAdapter(logger.ForContext(Constants.SourceContextPropertyName, name));
        }
    }

    internal class SeriLoggerAdapter : Microsoft.Owin.Logging.ILogger
    {
        private Serilog.ILogger _logger;

        public SeriLoggerAdapter(Serilog.ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _logger = logger;
        }

        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            LogEventLevel level = ToLogEventLevel(eventType);
            bool isEnabled = _logger.IsEnabled(level);
            if (state == null)
            {
                return isEnabled;
            }
            if (!isEnabled)
            {
                return false;
            }
            _logger.Write(level, exception, formatter(state, exception), eventId);
            return true;
        }

        private static LogEventLevel ToLogEventLevel(TraceEventType eventType)
        {
            LogEventLevel level;
            switch (eventType)
            {
                case TraceEventType.Critical:
                    level = LogEventLevel.Fatal;
                    break;
                case TraceEventType.Error:
                    level = LogEventLevel.Error;
                    break;
                case TraceEventType.Warning:
                    level = LogEventLevel.Warning;
                    break;
                case TraceEventType.Information:
                    level = LogEventLevel.Information;
                    break;
                case TraceEventType.Verbose:
                    level = LogEventLevel.Verbose;
                    break;
                case TraceEventType.Start:
                    level = LogEventLevel.Information;
                    break;
                case TraceEventType.Stop:
                    level = LogEventLevel.Information;
                    break;
                case TraceEventType.Suspend:
                    level = LogEventLevel.Information;
                    break;
                case TraceEventType.Resume:
                    level = LogEventLevel.Information;
                    break;
                case TraceEventType.Transfer:
                    level = LogEventLevel.Information;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventType));
            }
            return level;
        }
    }
}