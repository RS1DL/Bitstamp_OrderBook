namespace OrderBook.Shared.Models
{
    public record Bid
    {
        public float Price { get; init; }
        public float Amount { get; init; }
    }
}