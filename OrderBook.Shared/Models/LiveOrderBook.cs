namespace OrderBook.Shared.Models
{
    public class LiveOrderBook
    {
        public List<string[]> Bids { get; set; }
        public List<string[]> Asks { get; set; }
        public string TimeStamp { get; set; }
        public string MicroTimeStamp { get; set; }
    }
}
