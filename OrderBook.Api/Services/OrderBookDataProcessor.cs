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

        public OrderBookDataProcessor(IHubContext<OrderBookHub, IOrderBookClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProcessDataAsync(LiveOrderBook data)
        {
            await _hubContext.Clients.All.ReceiveOrderBookCurrentState(data);
        }
    }
}