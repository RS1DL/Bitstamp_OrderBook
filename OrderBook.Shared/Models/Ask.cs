namespace OrderBook.Shared.Models
{
    public record Ask
    {
        public float Price { get; init; }
        public float Amount { get; init; }
    }
}