using Gramr.Core.Models.Data;

namespace Gramr.Core.Interfaces.Api
{
    public interface IMarketDataRetrievalService
    {
        public Task<List<MarketAggregate>> GetAggregates(Company company, DateTime start, DateTime end);
    }
}
