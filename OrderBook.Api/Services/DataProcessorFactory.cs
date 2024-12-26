using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Api.Services
{
    public class DataProcessorFactory : IDataProcessorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DataProcessorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDataProcessor<T> Create<T>()
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                return scope.ServiceProvider.GetRequiredService<IDataProcessor<T>>();
            }
        }
    }
}