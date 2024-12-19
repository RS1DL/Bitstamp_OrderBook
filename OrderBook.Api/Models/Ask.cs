namespace OrderBook.Api.Models
{
    public record Ask
    {
        public float Price { get; init; }
        public float Amount { get; init; }
    }
}