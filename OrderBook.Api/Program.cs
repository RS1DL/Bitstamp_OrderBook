using OrderBook.Api.Infrastructure.Hub;
using OrderBook.Shared.Models;
using OrderBook.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddCors();
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());

builder.Services.AddTransient<IDataReceiver, OrderBookDataReceiver>();
builder.Services.AddTransient<IDataProcessor<LiveOrderBook>, OrderBookDataProcessor>();

builder.Services.AddHostedService<OrderBookDataReceiver>();

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

