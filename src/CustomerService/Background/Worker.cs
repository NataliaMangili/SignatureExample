using CustomerService.Events;
using EventBus.Abstractions;
using Helpers.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CustomerService.Background;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IEventBus _eventBus;

    public Worker(ILogger<Worker> logger, IEventBus eventBus)
    {
        _logger = logger;
        _eventBus = eventBus;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Inscrever-se para o evento
        _eventBus.Subscribe<CustomerEnrolledEvent, CustomerEnrolledEventHandler>();

        _logger.LogInformation("Worker listening to events...");

        // O método ExecuteAsync continuará rodando enquanto o serviço não for interrompido
        // O token de cancelamento é verificado durante a execução

        stoppingToken.ThrowIfCancellationRequested(); // Permite interromper o serviço caso seja solicitado

        return Task.CompletedTask;
    }
}
