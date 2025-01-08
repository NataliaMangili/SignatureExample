using LogService.Configurations;
using LogService.Models;
using MongoDB.Driver;

namespace LogService.Repositories;

public class LogRepository : ILogRepository
{
    private readonly IMongoCollection<LogEntry> _logsCollection;

    public LogRepository(MongoDbSettings settings)
    {
        MongoClient client = new MongoClient(settings.ConnectionString);
        IMongoDatabase database = client.GetDatabase(settings.DatabaseName);
        _logsCollection = database.GetCollection<LogEntry>(settings.LogsCollectionName);
    }

    public async Task SaveLogAsync(LogEntry logEntry)
    {
        await _logsCollection.InsertOneAsync(logEntry);
    }

    public async Task<IEnumerable<LogEntry>> GetLogsAsync(string source, string level)
    {
        FilterDefinition<LogEntry> filter = Builders<LogEntry>.Filter.Eq(log => log.Source, source) &
                Builders<LogEntry>.Filter.Eq(log => log.Level, level);

        return await _logsCollection.Find(filter).ToListAsync();
    }
}