namespace OrderBook.Api.Models
{
    public class LiveOrderBook
    {
        public List<Bid> Bids { get; set; }
        public List<Ask> Asks { get; set; }
        public DateTime TimeStamp { get; set; }
        public TimeOnly MicroTimeStamp { get; set; }
    }
}
