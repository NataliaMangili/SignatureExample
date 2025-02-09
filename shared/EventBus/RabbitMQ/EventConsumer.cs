using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventBus.RabbitMQ;

public class EventConsumer<TEvent, THandler> : EventingBasicConsumer
    where TEvent : IntegrationEvent
    where THandler : IIntegrationEventHandler<TEvent>
{
    private readonly IServiceProvider _serviceProvider;

    public EventConsumer(IModel model, IServiceProvider serviceProvider) : base(model)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task HandleAsync(TEvent @event)
    {
        var handler = _serviceProvider.GetRequiredService<THandler>(); // Resolve o handler via DI
        await handler.Handle(@event); // Processa o evento
    }
}