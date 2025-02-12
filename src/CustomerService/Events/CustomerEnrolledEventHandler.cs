using CustomerService.Models;
using CustomerService.Services;
using EventBus.Abstractions;
using Helpers.Events;
using System;

namespace CustomerService.Events;

public class CustomerEnrolledEventHandler(EmailService emailService /* AppDbContext dbContext*/) : IIntegrationEventHandler<CustomerEnrolledEvent>
{
    private readonly EmailService _emailService = emailService;

    public async Task Handle(CustomerEnrolledEvent @event)
    {
        Customer customer = new(
            name: "João Silva",
            email: "joao.silva@example.com",
            password: "Senha123"
        );

        // persistência
        // await _dbContext.Customers.AddAsync(customer);
        // await _dbContext.SaveChangesAsync();

        // Enviar email
    //    await _emailService.SendEmail(new Dictionary<string, string>
    //{
    //    { "name", @event.Name },
    //    { "email", @event.Email }
    //});

        // Tratar com log (TODO)
        Console.WriteLine($"Processed Event TESTE CUSTOMER : {nameof(CustomerEnrolledEvent)}");
    }
}