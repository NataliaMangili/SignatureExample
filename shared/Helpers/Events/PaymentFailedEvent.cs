namespace PaymentService.Events;

public class PaymentFailedEvent
{
    public string TransactionId { get; set; }
    public string Reason { get; set; }
}
