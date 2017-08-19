namespace Owin.Study.Legacy.Logging
{
    public interface ILogRepository
    {
        LogEntry[] GetLastLogs();
    }
}