using EventBus.Abstractions;
using Helpers.Events;
using PaymentService.Events.Handlers;

namespace PaymentService;

public class PaymentEventsListener(IEventBus eventBus, ILogger<PaymentEventsListener> logger)
{
    private readonly IEventBus _eventBus = eventBus;
    private readonly ILogger<PaymentEventsListener> _logger = logger;

    public void StartListeningForEvents()
    {
        // Inscreve-se para o evento de inscrição do cliente
        _eventBus.Subscribe<CustomerEnrolledEvent, PaymentEventHandler>();
        _logger.LogInformation("Signed up for the customer registration event");
    }
}
