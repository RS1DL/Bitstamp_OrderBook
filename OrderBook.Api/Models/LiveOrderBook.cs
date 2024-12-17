using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Api.Models
{
    public class LiveOrderBook
    {
        public List<float> Bids { get; set; }
        public List<float> Asks { get; set; }
        public DateTime TimeStamp { get; set; }
        public TimeOnly MicroTimeStamp { get; set; }
    }
}
