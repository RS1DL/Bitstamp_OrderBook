using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using OrderBook.Api.Models;
using System.Text;
using System.Text.Json;

namespace OrderBook.Api.Services
{
    public class OrderBookDataReceiver: IDisposable, IDataReceiver
    {
        private readonly ILogger<OrderBookDataReceiver> _logger;
        private readonly ClientWebSocket _client;
        private readonly BitstampSubscription _subscription;

        public OrderBookDataReceiver(ILogger<OrderBookDataReceiver> logger)
        {
            _logger = logger;
            _client = new ClientWebSocket();
            _subscription = new BitstampSubscription();
        }

        public async Task SubscribeAsync(string pair)
        {
            _subscription.Pair = pair;
            string jsonBody = _subscription.ToString();
            byte[] buffer = Encoding.UTF8.GetBytes(jsonBody);
            await _client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task ReceiveDataAsync()
        {
            var buffer = new byte[1024 * 4];

            while(_client.State == WebSocketState.Open)
            {
                var result = await _client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if(result.MessageType == WebSocketMessageType.Close)
                {
                    await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    string data = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    LiveOrderBook orderBook = JsonSerializer.Deserialize<LiveOrderBook>(data);
                    _logger.LogInformation($"Received order book data at {orderBook.TimeStamp} with {orderBook.Bids.Count} bids and {orderBook.Asks.Count} asks.");
                }
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
