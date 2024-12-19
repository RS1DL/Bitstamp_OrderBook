using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OrderBook.Shared.Models;

namespace OrderBook.Api.Infrastructure.Hub
{
    public class OrderBookHub : Hub<IOrderBookClient>
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }

    public interface IOrderBookClient
    {
        Task ReceiveOrderBookCurrentState(LiveOrderBook data);
    }
}