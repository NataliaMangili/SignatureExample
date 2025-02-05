using DeliveryService.Events.Handlers;
using EventBus.Abstractions;
using Helpers.Events;

namespace DeliveryService;

public class DeliveryEventsListener(IEventBus eventBus, ILogger<DeliveryEventsListener> logger)
{
    private readonly IEventBus _eventBus = eventBus;
    private readonly ILogger<DeliveryEventsListener> _logger = logger;

    public void StartListeningForEvents()
    {
        // Assinando o evento de pagamento processado para tratar a entrega
        _eventBus.Subscribe<PaymentProcessedEvent, DeliveryEventHandler>();
        _logger.LogInformation("Assinado/Recebido para o evento de pagamento processado.");
    }
}
