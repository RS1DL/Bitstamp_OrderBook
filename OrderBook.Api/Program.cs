using OrderBook.Api.Infrastructure.Hub;
using OrderBook.Shared.Models;
using OrderBook.Api.Services;
using Serilog;
using OrderBook.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddCors();
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());

builder.Services.AddTransient<IDataReceiver, OrderBookDataReceiver>();
builder.Services.AddTransient<IDataProcessor<BitstampLiveOrderBook>, OrderBookDataProcessor>();

builder.Services.AddHostedService<OrderBookDataReceiver>();

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .WriteTo.RollingFile(builder.Configuration.GetSection("Logger").GetValue<string>("Path") +"/log-{Date}.txt",
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:W}] {Message:lj}{NewLine}{Exception}", 
            retainedFileCountLimit: 14, 
            fileSizeLimitBytes: 1073741824
            )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.MapHub<OrderBookHub>("/orderbookhub");

app.Run();

