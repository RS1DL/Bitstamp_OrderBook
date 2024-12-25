using Microsoft.AspNetCore.SignalR;
using OrderBook.Api.Infrastructure.Hub;
using OrderBook.Api.Models;
using OrderBook.Shared.Models;
using OrderBook.Api.Extensions;
using OrderBook.Api.Infrastructure.DB;
using OrderBook.Api.Infrastructure.DB.Entities;

namespace OrderBook.Api.Services
{
    public class OrderBookDataProcessor : IDataProcessor<BitstampLiveOrderBook>
    {
        private readonly IHubContext<OrderBookHub, IOrderBookClient> _hubContext;
        private readonly OrderBookDbContext _dbContext;
        private readonly ILogger<OrderBookDataProcessor> _logger;

        public OrderBookDataProcessor(IHubContext<OrderBookHub, IOrderBookClient> hubContext, 
                                      OrderBookDbContext dbContext,
                                      ILogger<OrderBookDataProcessor> logger)
        {
            _hubContext = hubContext;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task ProcessDataAsync(Guid packageId, BitstampLiveOrderBook data)
        {
            try{
                LiveOrderBook orderBook = new LiveOrderBook
                {
                    Asks = data.Asks.ParseFromStrings().GroupOrders(),
                    Bids = data.Bids.ParseFromStrings().GroupOrders(),
                    TimeStamp = data.TimeStamp,
                    MicroTimeStamp = data.MicroTimeStamp
                };
                
                await _hubContext.Clients.All.ReceiveOrderBookCurrentState(orderBook);
                await SaveDataAsync(packageId, orderBook);
                _logger.LogInformation($"Data processed for batch {packageId} at {DateTime.UtcNow}: Asks: {orderBook.Asks.Count()} Bids: {orderBook.Bids.Count()}");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error processing data");
            }
        }

        private async Task SaveDataAsync(Guid id, LiveOrderBook data)
        {
            var orderBookEntity = new OrderBookEntity
            {
                OrderBookId = id,
                Asks = data.Asks.Select(x => new OrderEntity 
                { 
                    OrderId = Guid.NewGuid(),
                    OrderBookId = id, 
                    Type = OrderType.Ask, 
                    Price = x.Price, 
                    Amount = x.Amount 
                }),

                Bids = data.Bids.Select(x => new OrderEntity 
                {
                    OrderId = Guid.NewGuid(),
                    OrderBookId = id, 
                    Type = OrderType.Bid, 
                    Price = x.Price, 
                    Amount = x.Amount 
                }),

                TimeStamp = data.TimeStamp,
                MicroTimeStamp = data.MicroTimeStamp,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.OrderBooks.Add(orderBookEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}