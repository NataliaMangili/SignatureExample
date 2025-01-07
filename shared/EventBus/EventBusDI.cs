using EventBus.Abstractions;
using EventBus.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus;

public static class EventBusExtensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, string rabbitMqHost)
    {
        services.AddSingleton(sp =>
            new RabbitMQConnection(rabbitMqHost, "admin", "admin"));
        services.AddSingleton<IEventBus, RabbitMQEventBus>();

        return services;
    }
}