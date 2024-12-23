using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OrderBook.Api.Infrastructure.Hub;
using OrderBook.Api.Models;
using OrderBook.Shared.Models;

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
                    Asks = data.Asks.Select(a => new Order
                    {
                        Price = decimal.Parse(a[0], CultureInfo.InvariantCulture),
                        Amount = decimal.Parse(a[1], CultureInfo.InvariantCulture)
                    }).ToList(),

                    Bids = data.Bids.Select(b => new Order
                    {
                        Price = decimal.Parse(b[0], CultureInfo.InvariantCulture),
                        Amount = decimal.Parse(b[1], CultureInfo.InvariantCulture)
                    }).ToList(),

                    TimeStamp = data.TimeStamp,
                    MicroTimeStamp = data.MicroTimeStamp
                };
                

                //TODO: Add logic to process data
                await _hubContext.Clients.All.ReceiveOrderBookCurrentState(orderBook);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error processing data");
            }
        }
    }
}