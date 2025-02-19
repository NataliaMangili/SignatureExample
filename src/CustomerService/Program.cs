using CustomerService.Background;
using CustomerService.Events;
using CustomerService.Services;
using EventBus.Abstractions;
using EventBus.RabbitMQ;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do RabbitMQ
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

// Criando RabbitMQConnection com IOptions<RabbitMQSettings>
builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<IOptions<RabbitMQSettings>>();
    return new RabbitMQConnection(options);
});

// Registrando EventBus
builder.Services.AddSingleton<IEventBus, RabbitMQEventBus>();

// Handlers de Eventos e Workers
builder.Services.AddTransient<EmailService>();  // EmailService como Transient
builder.Services.AddTransient<CustomerEnrolledEventHandler>();  // CustomerEnrolledEventHandler como Transient
builder.Services.AddHostedService<Worker>();  // Worker como Transient

//configure service port to listen
builder.WebHost.ConfigureKestrel(op =>
{
    op.ListenAnyIP(5004, li =>
    {
        li.Protocols = HttpProtocols.Http2;
    });
});

// Configura os servi�os gRPC
builder.Services.AddGrpc();

var app = builder.Build();
// app.UseHttpsRedirection();

// Servi�o gRPC
app.MapGrpcService<CustomersService>();


app.Run();
