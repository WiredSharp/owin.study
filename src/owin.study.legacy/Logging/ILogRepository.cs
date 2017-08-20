using System.Threading.Tasks;

namespace Owin.Study.Legacy.Logging
{
    public interface ILogRepository
    {
        Task AddLogAsync(LogEntry log);

        LogEntry[] GetLastLogs();
    }
}