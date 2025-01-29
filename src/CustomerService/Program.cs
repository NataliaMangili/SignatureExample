using CustomerService.Background;
using CustomerService.Events;
using EventBus.Abstractions;
using EventBus.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.UseHttpsRedirection();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // Context
        //services.AddDbContext<AppDbContext>();

        // Configura o RabbitMQEventBus e dependências
        services.AddSingleton<RabbitMQConnection>(sp =>
        {
            return new RabbitMQConnection(
                hostContext.Configuration["RabbitMQ:HostName"],
                hostContext.Configuration["RabbitMQ:UserName"],
                hostContext.Configuration["RabbitMQ:Password"]
            );
        });

        services.AddSingleton<IEventBus, RabbitMQEventBus>();

        // Configura os Handlers de Eventos
        services.AddTransient<CustomerEnrolledEventHandler>();

        // Adiciona o Worker
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();


app.Run();

