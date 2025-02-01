using EventBus;

namespace Helpers.Events;

public class DeliveryConfirmedEvent : IntegrationEvent
{
    public string CustomerId { get; set; }  // ID do cliente
    public string DeliveryId { get; set; }  // ID do pedido de entrega
    public decimal Amount { get; set; }     // Valor do pagamento
}
