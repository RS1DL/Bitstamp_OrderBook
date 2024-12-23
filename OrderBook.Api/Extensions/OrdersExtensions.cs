using System.Globalization;
using OrderBook.Shared.Models;

namespace OrderBook.Api.Extensions
{
    public static class OrdersExtensions 
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

        public static IEnumerable<Order> ParseFromStrings(this IEnumerable<string[]> orders)
        {
            return orders.Select(a => new Order
                    {
                        Price = decimal.Parse(a[0], CultureInfo.InvariantCulture),
                        Amount = decimal.Parse(a[1], CultureInfo.InvariantCulture)
                    });
        }
    }
}
