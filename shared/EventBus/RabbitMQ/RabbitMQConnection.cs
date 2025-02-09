using EventBus.RabbitMQ;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;

public class RabbitMQConnection : IDisposable
{
    private readonly ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private bool _disposed;

    public RabbitMQConnection(IOptions<RabbitMQSettings> options)
    {
        var settings = options.Value;
        _connectionFactory = new ConnectionFactory
        {
            HostName = settings.HostName,
            Port = settings.Port,
            UserName = settings.UserName,
            Password = settings.Password
        };

        TryConnect();
    }

    public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

    public IModel CreateModel()
    {
        if (!IsConnected) { TryConnect(); }

        return _connection.CreateModel();
    }

    private void TryConnect()
    {
        if (IsConnected) { return; }

        try
        {
            _connection = _connectionFactory.CreateConnection();
        }
        catch (BrokerUnreachableException ex)
        {
            Console.WriteLine($"RabbitMQ Broker Unreachable: {ex.Message}");
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"RabbitMQ Socket Error: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"RabbitMQ IO Error: {ex.Message}");
        }
    }

    public void Dispose()
    {
        if (_disposed) { return; }

        _disposed = true;

        try
        {
            _connection?.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error disposing RabbitMQ connection: {ex.Message}");
        }
    }
}
