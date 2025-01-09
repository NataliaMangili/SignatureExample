using Grpc.Core;
using LogService.Models;
using LogService.Repositories;
using Helpers.Protos.LogService;

namespace LogService.Services;

public class LogGrpcService(ILogRepository logRepository) : LogServiceApi.LogServiceApiBase
{
    private readonly ILogRepository _logRepository = logRepository;

    public override async Task<LogResponse> SaveLog(LogRequest request, ServerCallContext context)
    {
        LogEntry logEntry = new()
        {
            Level = request.Level,
            Message = request.Message,
            Source = request.Source,
            Details = request.Details
        };

        await _logRepository.SaveLogAsync(logEntry);

        return new LogResponse { Message = "Log salvo com sucesso" };
    }

    public override async Task GetLogs(LogFilter request, IServerStreamWriter<LogMessage> responseStream, ServerCallContext context)
    {
        IEnumerable<LogEntry> logs = await _logRepository.GetLogsAsync(request.Source, request.Level);

        logs.ToList().ForEach(async log =>
        {
            LogMessage logMessage = new()
            {
                Id = log.Id,
                Timestamp = log.Timestamp.ToString("o"),
                Level = log.Level,
                Message = log.Message,
                Source = log.Source,
                Details = log.Details
            };

            await responseStream.WriteAsync(logMessage);
        });
    }
}