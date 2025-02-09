using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;
using System;

namespace EventBus.RabbitMQ;
public class RabbitMQEventBus : IEventBus
{
    private readonly RabbitMQConnection _connection;
    private readonly ILogger<RabbitMQEventBus> _logger;
    private readonly string _exchangeName = "event_bus";
    private readonly IServiceProvider _serviceProvider;
    private readonly IModel _channel;

    public RabbitMQEventBus(RabbitMQConnection connection, ILogger<RabbitMQEventBus> logger, IServiceProvider serviceProvider)
    {
        _connection = connection;
        _logger = logger;
        _serviceProvider = serviceProvider;

        // Pegar a conexão já estabelecida e criar um modelo (canal)
        _channel = _connection.CreateModel();

        // Configura a exchange RabbitMQ (tipo fanout para enviar a mensagem para todas as filas)
        _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);

        _logger.LogInformation("Exchange configurada: {ExchangeName}", _exchangeName);
    }

    public void Publish(IntegrationEvent @event)
    {
        string eventName = @event.GetType().Name; // Nome do evento
        string message = JsonSerializer.Serialize(@event); // Serializa o evento para JSON
        byte[] body = Encoding.UTF8.GetBytes(message); // Converte para byte array

        // Publica a mensagem na exchange
        _channel.BasicPublish(
            exchange: _exchangeName,
            routingKey: "", // Com fanout, não usamos routingKey
            basicProperties: null,
            body: body);

        _logger.LogInformation("Evento publicado: {EventName}", eventName);
    }

    public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
    {
        string queueName = $"{typeof(T).Name}_{Guid.NewGuid()}"; // Nome único da fila para cada consumidor (microserviço)

        // Declara a fila do microserviço (cada microserviço terá uma fila diferente)
        _channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,  // Fila não exclusiva, permitindo múltiplos consumidores
            autoDelete: false,
            arguments: null);

        // Faz o binding da fila à exchange fanout
        _channel.QueueBind(
            queue: queueName,
            exchange: _exchangeName,
            routingKey: "");

        // Cria o consumidor
        EventConsumer<T, TH> consumer = new EventConsumer<T, TH>(_channel, _serviceProvider);

        // Aqui, ao invés de usar 'EventingBasicConsumer', usamos 'EventConsumer' diretamente
        consumer.Received += async (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body); // Converte a mensagem para string

            try
            {
                T? @event = JsonSerializer.Deserialize<T>(message); // Deserializa para o tipo do evento
                await consumer.HandleAsync(@event); // Chama o método HandleAsync do consumidor
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar evento: {EventName}", typeof(T).Name);
            }
        };

        // Inicia o consumo das mensagens da fila
        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
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