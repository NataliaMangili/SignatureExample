using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using CustomerService;
using Grpc.Core;
using Helpers.Channels;
using Helpers.Dtos;

namespace SignatureExampleAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{  
    private readonly IConfiguration _configuration;
    private readonly GrpcChannel _channel;
    private readonly CustomerServiceGRPC.CustomerServiceGRPCClient _client;

    public CustomerController(IConfiguration configuration)
    {
        _configuration = configuration;
        // Cria o canal gRPC usando o método Helper e o cliente (stub) para o serviço CustomerServiceGRPC
        _channel = ChannelHelperExtension.CreateGrpcChannel(_configuration, "CustomerService");
        _client = new CustomerServiceGRPC.CustomerServiceGRPCClient(_channel);
    }

    // Controller leva para o CustomersService
    // Que lança o StudentEnrolledEvent e chega ao StudentEnrolledEventHandler (Do Payment e Customer Services)
    // Ele chama EnrollClient.Invoke(data), que agenda "process_payment"
    // O Worker escuta os eventos e ativa PaymentEventHandler
    // O PaymentService salva o pagamento no banco
    // O EmailService pode enviar um e-mail confirmando o pagamento
    [HttpPost("enroll")]
    public async Task<IActionResult> EnrollCustomer([FromBody] EnrollCustomerRequest request)
    {
        try
        {
            EnrollCustomerResponse response = await _client.EnrollCustomerAsync(request);
            return Ok();
        }
        catch (RpcException ex)
        {
            return StatusCode(500, $"Erro ao chamar o serviço: {ex.Status.Detail}");
        }
    }
}
