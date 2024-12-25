using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Api.Services
{
    public interface IDataProcessor<T>
    {
        public Task ProcessDataAsync(Guid packageId, T data);
    }
}