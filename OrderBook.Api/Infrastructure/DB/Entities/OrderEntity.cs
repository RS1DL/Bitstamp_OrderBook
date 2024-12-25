using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderBook.Api.Infrastructure.DB.Entities
{
    public class OrderEntity
    {
        [Key]
        public Guid OrderId { get; set; }

        [ForeignKey("OrderBookEntity")]
        public Guid OrderBookId { get; set; }
        public OrderType Type { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }

    public enum OrderType
    {
        Bid,
        Ask
    }
}