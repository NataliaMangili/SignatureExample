namespace DeliveryService.Models;

public class Delivery(Guid customerId, decimal amount)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; } = customerId;
    public decimal Amount { get; set; } = amount;
    public DateTime DeliveryDate { get; set; } = DateTime.UtcNow;
    public string DeliveryStatus { get; set; } = "Pending";
}
