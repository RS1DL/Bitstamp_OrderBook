using Microsoft.AspNetCore.SignalR;
using OrderBook.Api.Services;

namespace OrderBook.Api.Infrastructure.Hub
{
    public class OrderBookHub : Hub<IOrderBookClient>
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
    }
}