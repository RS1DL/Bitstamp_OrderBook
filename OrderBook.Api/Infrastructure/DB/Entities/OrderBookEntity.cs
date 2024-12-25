using System.ComponentModel.DataAnnotations;

namespace OrderBook.Api.Infrastructure.DB.Entities
{
    public class OrderBookEntity
    {
        [Key]
        public Guid OrderBookId { get; set; }
        public IEnumerable<OrderEntity> Bids { get; set; }
        public IEnumerable<OrderEntity> Asks { get; set; }
        public string TimeStamp { get; set; }
        public string MicroTimeStamp { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}