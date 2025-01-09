using LogService.Configurations;
using LogService.Repositories;
using LogService.Services;
//using LogService.Services;

var builder = WebApplication.CreateBuilder(args);

//MongoDB
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<ILogRepository, LogRepository>();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<LogGrpcService>();

app.MapGet("/", () => "Use gRPC");

app.Run();