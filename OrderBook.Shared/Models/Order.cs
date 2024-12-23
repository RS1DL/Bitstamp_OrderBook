namespace OrderBook.Shared.Models
{
    public record Order
    {
        public decimal Price { get; init; }
        public decimal Amount { get; init; }
    }
}