using OrderBook.Shared.Models;

namespace OrderBook.Api.Services
{
    public interface IOrderBookClient
    {
        Task ReceiveOrderBookCurrentState(LiveOrderBook data);
    }
}