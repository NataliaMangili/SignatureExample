using PaymentService;
using PaymentService.Data;
using PaymentService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        //services.AddDbContext<PaymentDbContext>(); // Banco de Dados
        services.AddScoped<PaymentRepository>();   // Reposit�rio
        //services.AddScoped<PaymentService>();      // Servi�o de Pagamento
        services.AddScoped<EnrollCustomer>();        // Inscri��o do Cliente
        services.AddScoped<Scheduler>();           // Agendador de Tarefas
        services.AddScoped<EmailService>();        // Servi�o de Email
        services.AddHostedService<Worker>();       // Worker para eventos
    })
    .Build();

await host.RunAsync();

app.Run();
