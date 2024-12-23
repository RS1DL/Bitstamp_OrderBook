using Microsoft.AspNetCore.SignalR;
using OrderBook.Api.Infrastructure.Hub;
using OrderBook.Api.Models;
using OrderBook.Shared.Models;
using OrderBook.Web.Extensions;

namespace OrderBook.Api.Services
{
    public class OrderBookDataProcessor : IDataProcessor<BitstampLiveOrderBook>
    {
        private readonly IHubContext<OrderBookHub, IOrderBookClient> _hubContext;
        private readonly ILogger<OrderBookDataProcessor> _logger;

        public OrderBookDataProcessor(IHubContext<OrderBookHub, IOrderBookClient> hubContext, 
                                      ILogger<OrderBookDataProcessor> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task ProcessDataAsync(BitstampLiveOrderBook data)
        {
            _logger.LogInformation($"Processing data for batch {data.TimeStamp}");
            try{
                LiveOrderBook orderBook = new LiveOrderBook
                {
                    Asks = data.Asks.ParseFromStrings().GroupOrders(),
                    Bids = data.Bids.ParseFromStrings().GroupOrders(),
                    TimeStamp = data.TimeStamp,
                    MicroTimeStamp = data.MicroTimeStamp
                };
                
                await _hubContext.Clients.All.ReceiveOrderBookCurrentState(orderBook);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error processing data");
            }
        }
    }
}