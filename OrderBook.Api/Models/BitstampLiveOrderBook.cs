namespace OrderBook.Api.Models
{
    public record BitstampLiveOrderBook
    {
        public IEnumerable<string[]> Bids { get; init; }
        public IEnumerable<string[]> Asks { get; init; }
        public string TimeStamp { get; init; }
        public string MicroTimeStamp { get; init; }
    }
}