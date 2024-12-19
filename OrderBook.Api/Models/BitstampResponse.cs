using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderBook.Shared.Models;

namespace OrderBook.Api.Models
{
    public class BitstampResponse
    {
        public LiveOrderBook Data { get; set; }
        public string Channel { get; set; }
        public string Event { get; set; }
    }
}