using EventBus.Abstractions;
using Helpers.Events;

namespace PaymentService.Services;

public class PaymentService(IEventBus eventBus, ILogger<PaymentService> logger)
{
    private readonly IEventBus _eventBus = eventBus;
    private readonly ILogger<PaymentService> _logger = logger;

    // Simula a lógica de processamento de pagamento
    public void ProcessPayment(string customerId, string deliveryId, decimal amount)
    {
        //_paymentRepository.Save();
        var transactionId = Guid.NewGuid().ToString();

        _logger.LogInformation($"Payment processed for customer: {customerId}, amount: {amount}");

        // Criando o evento de pagamento processado
        var paymentProcessedEvent = new PaymentProcessedEvent
        {
            CustomerId = customerId,
            DeliveryId = deliveryId,
            Amount = amount,
            TransactionId = transactionId
        };

        // Publicando o evento no EventBus
        _eventBus.Publish(paymentProcessedEvent);
        _logger.LogInformation($"Payment processed event published to the DeliveryService");
    }
}
