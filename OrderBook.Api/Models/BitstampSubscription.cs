using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Api.Models
{
    public class BitstampSubscription
    {
        public string Event { get; set; } = "bts:subscribe";
        public string Channel { get; set; } = "order_book";
        public string Pair { get; set; } = "btceur";

        public override string ToString()
        {
            string jsonBody = /*lang=json,strict*/ """
            {
                "event": "{Event}",
                "data": {
                    "channel": "{Channel}_{Pair}"
                }
            }
            """;

            return jsonBody;
        }
    }
}
