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
            try{
                _logger.LogInformation($"Processing data for batch {data.TimeStamp}");
                _logger.LogInformation($"Data first bid {data.Bids.First()[0]} {data.Bids.First()[1]}");
                await _hubContext.Clients.All.ReceiveOrderBookCurrentState(data);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error processing data");
            }
        }
    }
}