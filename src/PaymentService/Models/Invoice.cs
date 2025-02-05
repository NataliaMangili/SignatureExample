namespace PaymentService.Models;

public class Invoice
{
    public int Id { get; set; }
    public string StudentId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
}
