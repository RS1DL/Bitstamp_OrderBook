using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Shared.Models
{
    public class BitstampEvent
    {
        public BitstampEvent(string _event, string channel, string pair)
        {
            Event = _event;
            Channel = channel;
            Pair = pair;
        }

        public BitstampEvent(string _event): this(_event, "order_book", "btceur") { }
        public BitstampEvent(): this("bts:subscribe", "order_book", "btceur") { }

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

            jsonBody = jsonBody.Replace("{Event}", Event);
            jsonBody = jsonBody.Replace("{Channel}", Channel);
            jsonBody = jsonBody.Replace("{Pair}", Pair);

            return jsonBody;
        }
    }
}
