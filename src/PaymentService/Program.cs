using EventBus.Abstractions;
using EventBus.RabbitMQ;
using Microsoft.Extensions.Options;
using PaymentService;
using PaymentService.Data;
using PaymentService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // Banco de Dados e Repositório
        // services.AddDbContext<PaymentDbContext>();
        services.AddScoped<PaymentRepository>(); // Scoped para repositório de pagamentos

        // services.AddScoped<PaymentService>(); 
        services.AddTransient<EnrollCustomer>(); 
        services.AddTransient<CustomerEnrolledEventHandler>(); 
        services.AddScoped<Scheduler>();
        services.AddScoped<EmailService>();

        // Configuração do RabbitMQ
        services.Configure<RabbitMQSettings>(hostContext.Configuration.GetSection("RabbitMQ"));

        // Criando RabbitMQConnection com IOptions<RabbitMQSettings>
        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<RabbitMQSettings>>();
            return new RabbitMQConnection(options);
        });

        // Registrando EventBus como Singleton
        services.AddSingleton<IEventBus, RabbitMQEventBus>();

        // Worker para eventos (vem depois do EventBus)
        services.AddHostedService<Worker>(); // Serviço em segundo plano que processa eventos
    })
    .Build();


await host.RunAsync();

app.Run();
