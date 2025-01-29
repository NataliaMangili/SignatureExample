using CustomerService.Models;
using CustomerService.Services;
using EventBus.Abstractions;

namespace CustomerService.Events;

public class CustomerEnrolledEventHandler(/*AppDbContext dbContext,*/ EmailService emailService) : IIntegrationEventHandler<CustomerEnrolledEvent>
{
    //private readonly AppDbContext _dbContext;
    private readonly EmailService _emailService = emailService;

    public async Task Handle(CustomerEnrolledEvent @event)
    {
        Customer customer = new(
            name: "João Silva",
            email: "joao.silva@example.com", 
            password: "Senha123"
        );

        //await _dbContext.Customers.AddAsync(Customer);
        //await _dbContext.SaveChangesAsync();

        // Enviar email
        await _emailService.SendEmail(new Dictionary<string, string>
        {
            { "name", @event.Name },
            { "email", @event.Email }
        });

        //tratar com LOG TODO
        Console.WriteLine($"Processed Event : {nameof(CustomerEnrolledEvent)}");
    }
}