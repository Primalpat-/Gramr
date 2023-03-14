using Gramr.Core.Models.Data;

namespace Gramr.Core.Interfaces.Data.Services
{
    public interface IMarketDataManagementService
    {
        Task<List<MarketAggregate>> GetData(Company company);
    }
}
