namespace PaymentService.Services;

//Agendamento de Tarefas em segundo plano
public class Scheduler(ILogger<Scheduler> logger)
{
    private readonly ILogger<Scheduler> _logger = logger;

    public void Schedule(string taskName, object data)
    {
        //Logica da tarefa em segundo plano
        _logger.LogInformation($"Task {taskName} scheduled with data: {data}");
    }
}
