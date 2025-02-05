namespace PaymentService.Models;

public class Transaction
{
    public int Id { get; set; }
    public string PaymentId { get; set; }
    public DateTime Timestamp { get; set; }
}
