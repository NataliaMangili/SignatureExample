namespace EventBus.Events;

public abstract record IntegrationEvent
{
    public Guid Id { get; private init; }
    public DateTime CreationDate { get; private init; }

    protected IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }
}