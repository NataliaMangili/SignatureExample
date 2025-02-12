using CustomerService.Models;
using EventBus.Abstractions;
using Grpc.Core;
using Helpers.Events;

namespace CustomerService.Services;

public class CustomersService(IEventBus eventBus) : CustomerServiceGRPC.CustomerServiceGRPCBase
{
    private readonly IEventBus _eventBus = eventBus;

    public override async Task<EnrollCustomerResponse> EnrollCustomer(EnrollCustomerRequest customerDto, ServerCallContext context)
    {
        // Lógica de inscrição do cliente (chamar um repos para salvar no banco de dados, etc.) e validação de salvamento
        Customer newCustomer = new(customerDto.Name, customerDto.Email, customerDto.Password);
        //await _customerRepository.AddAsync(newCustomer);

        // Enviar evento para o EventBus após a inscrição
        var customerEnrolledEvent = new CustomerEnrolledEvent
        {
            CustomerId = newCustomer.Id.ToString(),
            Name = newCustomer.Name,
            Email = newCustomer.Email
        };

        _eventBus.Publish(customerEnrolledEvent);

        return new EnrollCustomerResponse();
    }
}
