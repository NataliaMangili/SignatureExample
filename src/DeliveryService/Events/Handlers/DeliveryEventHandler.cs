using DeliveryService.Models;
using EventBus.Abstractions;
using Helpers.Events;

namespace DeliveryService.Events.Handlers;

public class DeliveryEventHandler(ILogger<DeliveryEventHandler> logger) : IIntegrationEventHandler<PaymentProcessedEvent>
{
    private readonly ILogger<DeliveryEventHandler> _logger = logger;

    public async Task Handle(PaymentProcessedEvent @event)
    {
        // Lógica de criação de entrega
        _logger.LogInformation($"Processando entrega para o cliente {@event.CustomerId}");

        var delivery = new Delivery(Guid.Parse(@event.CustomerId), @event.Amount);

        await SaveDeliveryAsync(delivery);

        _logger.LogInformation($"Entrega para o cliente {@event.CustomerId} foi iniciada com sucesso.");
    }

    private Task SaveDeliveryAsync(Delivery delivery)
    {
        // Lógica de salvar a entrega no banco de dados (ou em outro serviço)
        return Task.CompletedTask;
    }
}