using LogService.Models;

namespace LogService.Repositories;

public interface ILogRepository
{
    Task SaveLogAsync(LogEntry logEntry);
    Task<IEnumerable<LogEntry>> GetLogsAsync(string source, string level);
}