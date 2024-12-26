using Microsoft.EntityFrameworkCore;
using OrderBook.Api.Infrastructure.Hub;
using OrderBook.Api.Services;
using Serilog;
using OrderBook.Api.Models;
using OrderBook.Api.Infrastructure.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddCors();
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddConsole());

builder.Services.AddTransient<IDataReceiver, OrderBookDataReceiver>();
builder.Services.AddScoped<IDataProcessor<BitstampLiveOrderBook>, OrderBookDataProcessor>();
builder.Services.AddSingleton<IDataProcessorFactory, DataProcessorFactory>();

var orderBookDbConnectionString = Environment.GetEnvironmentVariable("ORDER_BOOK_DB") ?? builder.Configuration["OrderBook:ConnectionString"];

builder.Services.AddDbContextFactory<OrderBookDbContext>(options => 
    options.UseNpgsql(orderBookDbConnectionString));

builder.Services.AddHostedService<OrderBookDataReceiver>();

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.PostgreSQL(orderBookDbConnectionString, "Logs", needAutoCreateTable: true)
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

