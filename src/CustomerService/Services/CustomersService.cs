using CustomerService.Events;
using CustomerService.Models;
using EventBus.Abstractions;
using Helpers.Events;

namespace CustomerService.Services;

public class CustomersService(IEventBus eventBus)
{
    private readonly IEventBus _eventBus = eventBus;

    public void EnrollCustomer(Customer customer)
    {
        // Lógica de inscrição do cliente (salvar no banco de dados, etc.)

        // Enviar evento para o EventBus após a inscrição
        var customerEnrolledEvent = new CustomerEnrolledEvent
        {
            CustomerId = customer.Id.ToString(),
            Name = customer.Name,
            Email = customer.Email
        };

        _eventBus.Publish(customerEnrolledEvent);
    }
}
