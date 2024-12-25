using Microsoft.EntityFrameworkCore;
using OrderBook.Api.Infrastructure.DB.Entities;

namespace OrderBook.Api.Infrastructure.DB
{
    public class OrderBookDbContext : DbContext
    {
        public OrderBookDbContext(DbContextOptions<OrderBookDbContext> options) : base(options) { }
        public DbSet<OrderBookEntity> OrderBooks { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
    }
}