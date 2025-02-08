namespace PaymentService.Services;

//Agendamento de Tarefas em segundo plano
public class Scheduler(ILogger<Scheduler> logger)
{
    private readonly ILogger<Scheduler> _logger = logger;

    public void Schedule(string taskName, object data)
    {
        // Log para o agendamento da tarefa
        _logger.LogInformation($"Task {taskName} scheduled with data: {data}");

        // Simula o agendamento: após 5 segundos, a tarefa é "executada"
        Task.Run(async () =>
        {
            // Aguarda 5 segundos para simular o delay do agendamento
            await Task.Delay(5000);

            _logger.LogInformation($"Task {taskName} executed with data: {data}");
            // Lógica real da tarefa que deve ser executada
        });
    }
}
