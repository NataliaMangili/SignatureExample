using EventBus;

namespace CustomerService.Events;

public class CustomerEnrolledEvent : IntegrationEvent
{
    public string Name { get; set; }
    public string Email { get; set; }
}