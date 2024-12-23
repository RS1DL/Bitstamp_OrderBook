namespace OrderBook.Shared.Models
{
    public class LiveOrderBook
    {
        public List<Order> Bids { get; set; }
        public List<Order> Asks { get; set; }
        public string TimeStamp { get; set; }
        public string MicroTimeStamp { get; set; }
    }
}
