using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogService.Models;

public class LogEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("Timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    [BsonElement("Level")]
    public string Level { get; set; } = null!;

    [BsonElement("Message")]
    public string Message { get; set; } = null!;

    [BsonElement("Source")]
    public string Source { get; set; } = null!;

    [BsonElement("Details")]
    public string? Details { get; set; }
}