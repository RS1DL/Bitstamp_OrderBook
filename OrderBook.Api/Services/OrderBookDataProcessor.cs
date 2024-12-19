using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OrderBook.Api.Infrastructure.Hub;
using OrderBook.Shared.Models;

namespace OrderBook.Api.Services
{
    public class OrderBookDataProcessor : IDataProcessor<LiveOrderBook>
    {
        private readonly IHubContext<OrderBookHub, IOrderBookClient> _hubContext;
        private readonly ILogger<OrderBookDataProcessor> _logger;

        public OrderBookDataProcessor(IHubContext<OrderBookHub, IOrderBookClient> hubContext, 
                                      ILogger<OrderBookDataProcessor> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task ProcessDataAsync(LiveOrderBook data)
        {
            _logger.LogInformation($"Processing data for batch {data.TimeStamp}");
            await _hubContext.Clients.All.ReceiveOrderBookCurrentState(data);
        }
    }
}