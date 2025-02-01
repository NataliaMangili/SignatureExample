using EventBus;

namespace Helpers.Events;

public class CustomerEnrolledEvent : IntegrationEvent
{
    public string CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}