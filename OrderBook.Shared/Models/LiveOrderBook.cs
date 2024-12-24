namespace OrderBook.Shared.Models
{
    public class LiveOrderBook
    {
        public IEnumerable<Order> Bids { get; set; }
        public IEnumerable<Order> Asks { get; set; }
        public string TimeStamp { get; set; }
        public string MicroTimeStamp { get; set; }
    }
}
