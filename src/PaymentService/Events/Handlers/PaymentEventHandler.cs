using EventBus.Abstractions;
using Helpers.Events;

namespace PaymentService.Events.Handlers;

public class PaymentEventHandler(IEventBus eventBus, ILogger<PaymentEventHandler> logger) : IIntegrationEventHandler<CustomerEnrolledEvent>
{
    private readonly IEventBus _eventBus = eventBus;
    private readonly ILogger<PaymentEventHandler> _logger = logger;

    public async Task Handle(CustomerEnrolledEvent @event)
    {
        // Lógica de processamento do pagamento
        _logger.LogInformation($"Processing payment for customer {@event.CustomerId}");

        // Lógica do pagamento
        var paymentProcessedEvent = new PaymentProcessedEvent
        {
            CustomerId = @event.CustomerId,
            Amount = 100.00m, // Exemplo
            PaymentDate = DateTime.UtcNow
        };

        // Publica o evento PaymentProcessedEvent para notificar outros microserviços
        _eventBus.Publish(paymentProcessedEvent);

        _logger.LogInformation("Payment processed and event sent.");
    }
}

