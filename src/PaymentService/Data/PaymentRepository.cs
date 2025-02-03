using PaymentService.Models;

namespace PaymentService.Data;

//Logica de acesso ao banco para salvar as classes e get delas
public class PaymentRepository
{
    private readonly List<Payment> _payments = [];

    public void Save(Payment payment) => _payments.Add(payment);

    public List<Payment> GetAll() => _payments;
}
