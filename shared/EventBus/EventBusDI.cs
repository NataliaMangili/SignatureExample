using EventBus.Abstractions;
using EventBus.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus;

public static class EventBusExtensions
{
    public static IServiceCollection AddEventBus(this IServiceCollection services)
    {
        // Registrando RabbitMQConnection usando as configurações do Options Pattern
        services.AddSingleton<RabbitMQConnection>();

        services.AddSingleton<IEventBus, RabbitMQEventBus>();

        return services;
    }
}
