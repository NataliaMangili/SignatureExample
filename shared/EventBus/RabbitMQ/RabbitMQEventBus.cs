using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System.Text;
using System.Text.Json;

namespace EventBus.RabbitMQ;

public class RabbitMQEventBus : IEventBus
{
    private readonly RabbitMQConnection _connection;
    private readonly ILogger<RabbitMQEventBus> _logger;
    private readonly string _exchangeName = "event_bus";
    private readonly IModel _channel;

    public RabbitMQEventBus(RabbitMQConnection connection, ILogger<RabbitMQEventBus> logger)
    {
        _connection = connection;
        _logger = logger;
        _channel = _connection.CreateConnection().CreateModel();

        // Configura a exchange RabbitMQ
        _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);

        _logger.LogInformation("Exchange configurada: {ExchangeName}", _exchangeName);
    }

    public void Publish(IntegrationEvent @event)
    {
        string eventName = @event.GetType().Name; // Get Nome do evento
        string message = JsonSerializer.Serialize(@event); // Serializa o evento para JSON
        byte[] body = Encoding.UTF8.GetBytes(message); // Converte para byte array

        // Publica msgm na exchange
        _channel.BasicPublish(
            exchange: _exchangeName,
            routingKey: "",
            basicProperties: null,
            body: body);

        _logger.LogInformation("Evento publicado: {EventName}", eventName);
    }

    public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        string queueName = typeof(T).Name; // Nome da fila igual ao nome do evento

        // Declara a fila e faz o binding à exchange
        _channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _channel.QueueBind(
            queue: queueName,
            exchange: _exchangeName,
            routingKey: "");

        // Cria o consumidor
        EventingBasicConsumer consumer = new(_channel);

        consumer.Received += async (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body); // Converte a mensagem para string

            try
            {
                T? @event = JsonSerializer.Deserialize<T>(message); // Deserializa para o tipo do evento
                TH handler = Activator.CreateInstance<TH>(); // Cria instância do handler
                await handler.Handle(@event); // Processa o evento

                _logger.LogInformation("Evento processado: {EventName}", typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar evento: {EventName}", typeof(T).Name);
            }
        };

        // Consumir mensagens da fila
        _channel.BasicConsume(
            queue: queueName,
            autoAck: true,
            consumer: consumer);

        _logger.LogInformation("Assinatura criada para o evento: {EventName}", queueName);
    }

    public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        string queueName = typeof(T).Name;

        // Excluir a fila se necessário
        _channel.QueueDelete(queueName);

        _logger.LogInformation("Assinatura removida para o evento: {EventName}", queueName);
    }
}