using OrderBook.Shared.Models;

namespace OrderBook.Web.Extensions
{
    public static class Extensions 
    {
        public static IEnumerable<Order> GroupOrders(this IEnumerable<Order> orders)
        {
            return orders.GroupBy(o => o.Price)
                         .Select(g => new Order
                         {
                             Price = g.Key,
                             Amount = g.Sum(o => o.Amount)
                         });
        }
    }
}