namespace OrderBook.Tests;

using OrderBook.Api.Models;

public class ApiModelsTests
{
    [Fact]
    public void DefaulCreatedtBitstampSubscription_IsValidToString_True()
    {
        var bitstampSubscription = new BitstampSubscription();
        var expected = """
            {
                "event": "bts:subscribe",
                "data": {
                    "channel": "order_book_btceur"
                }
            }
            """.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
        
        Assert.Equal(expected, bitstampSubscription.ToString().Replace("\r\n", "").Replace("\r", "").Replace("\n", ""));
    }
}
