using EventBus;

namespace Helpers.Events;

public class PaymentProcessedEvent : IntegrationEvent
{
    public string CustomerId { get; set; }  // ID do cliente
    public string DeliveryId { get; set; }  // ID do pedido de entrega
    public decimal Amount { get; set; }     // Valor pago
    public string TransactionId { get; set; }  // ID da transação pagamento
    public DateTime PaymentDate { get; set; }
}
