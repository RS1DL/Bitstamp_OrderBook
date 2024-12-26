namespace OrderBook.Api.Services
{
    public interface IDataProcessorFactory
    {
        IDataProcessor<T> Create<T>();
    }
}