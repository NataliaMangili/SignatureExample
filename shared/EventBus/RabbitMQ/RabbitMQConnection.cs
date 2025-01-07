using RabbitMQ.Client;

namespace EventBus.RabbitMQ;

public class RabbitMQConnection
{
    private readonly ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private bool _disposed;

    public RabbitMQConnection(string hostName, string userName, string password)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = hostName,
            UserName = userName,
            Password = password
        };
    }

    public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

    public IConnection CreateConnection()
    {
        if (!IsConnected)
        {
            _connection = _connectionFactory.CreateConnection();
        }

        return _connection;
    }

    public void Dispose()
    {
        if (_disposed) return;

        _connection?.Dispose();
        _disposed = true;
    }
}