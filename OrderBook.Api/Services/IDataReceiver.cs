using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Api.Services
{
    public interface IDataReceiver
    {
        public Task SubscribeAsync(string pair);
        public Task ReceiveDataAsync();
    }
}
