using EventBus.Abstractions;
using Helpers.Dtos;
using Helpers.Events;
using PaymentService.Services;

public class CustomerEnrolledEventHandler(/*ILogger<CustomerEnrolledEventHandler> logger,*/ EnrollCustomer enrollClient) : IIntegrationEventHandler<CustomerEnrolledEvent>
{
    //private readonly ILogger<CustomerEnrolledEventHandler> _logger = logger;
    private readonly EnrollCustomer _enrollClient = enrollClient;

    public async Task Handle(CustomerEnrolledEvent customerEvent)
    {
        //_logger.LogInformation($"New customer subscribed: {customerEvent.CustomerId}");
        Console.WriteLine($"Processed Event TESTE PAYMENT : {nameof(CustomerEnrolledEvent)}");

        _enrollClient.Invoke(new EnrollCustomerInputData
        {
            CustomerId = customerEvent.CustomerId,
            DeliveryId = customerEvent.Name
        });

    }
}
