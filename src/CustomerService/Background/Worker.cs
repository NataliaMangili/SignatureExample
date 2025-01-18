using EventBus.Abstractions;
using CustomerService.Events;

namespace CustomerService.Background;

public class Worker(ILogger<Worker> logger, IEventBus eventBus) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly IEventBus _eventBus = eventBus;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Assinar eventos
        _eventBus.Subscribe<CustomerEnrolledEvent, CustomerEnrolledEventHandler>();

        _logger.LogInformation("Worker listening events...");
        return Task.CompletedTask;
    }
}