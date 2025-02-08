using EventBus.Abstractions;
using Helpers.Events;
using PaymentService.Events.Handlers;

namespace PaymentService;

public class Worker(ILogger<Worker> logger, IEventBus eventBus) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly IEventBus _eventBus = eventBus;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker iniciado. Aguardando eventos...");

        // Assinando eventos para que o Worker escute
        _eventBus.Subscribe<CustomerEnrolledEvent, CustomerEnrolledEventHandler>();

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken); // Aguarda a próxima verificação
        }
    }
}