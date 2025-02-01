namespace Helpers.Dtos;

public class EnrollCustomerInputData
{
    public string CustomerId { get; set; }
    public string DeliveryId { get; set; }
    public decimal Amount { get; set; }     // Valor do pagamento
    public string Currency { get; set; } = "BRL";
    public string DeliveryAddress { get; set; } // Endereço de entrega
    public string DeliveryStatus { get; set; }  // Status do pedido
}
