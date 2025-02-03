using Helpers.Dtos;

namespace PaymentService.Services;

//Chama a tarefa em segundo pano
public class EnrollCustomer(Scheduler taskScheduler, ILogger<EnrollCustomer> logger)
{
    private readonly Scheduler _taskScheduler = taskScheduler;
    private readonly ILogger<EnrollCustomer> _logger = logger;

    public void Invoke(EnrollCustomerInputData data)
    {
        _logger.LogInformation($"Scheduling payment for customer {data.CustomerId}");
        _taskScheduler.Schedule("process_payment", data);
    }
}
