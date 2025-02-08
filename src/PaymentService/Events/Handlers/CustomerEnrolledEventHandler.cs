using EventBus.Abstractions;
using Helpers.Dtos;
using Helpers.Events;
using PaymentService.Services;

namespace PaymentService.Events.Handlers;

// CustomersService lança o StudentEnrolledEvent e chega ao StudentEnrolledEventHandler
// Ele chama EnrollClient.Invoke(data), que agenda "process_payment"
// O Worker escuta os eventos e ativa PaymentEventHandler
// O PaymentService salva o pagamento no banco
// O EmailService pode enviar um e-mail confirmando o pagamento
public class CustomerEnrolledEventHandler(ILogger<CustomerEnrolledEventHandler> logger, EnrollCustomer enrollClient) : IIntegrationEventHandler<CustomerEnrolledEvent>
{
    private readonly ILogger<CustomerEnrolledEventHandler> _logger = logger;
    private readonly EnrollCustomer _enrollClient = enrollClient;

    public async Task Handle(CustomerEnrolledEvent customerEvent)
    {
        _logger.LogInformation($"New customer subscribed: {customerEvent.CustomerId}");
        _enrollClient.Invoke(new EnrollCustomerInputData
        {
            CustomerId = customerEvent.CustomerId,
            DeliveryId = customerEvent.Name
        });
    }
}