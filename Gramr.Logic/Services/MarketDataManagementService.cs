using Gramr.Core.Interfaces.Api;
using Gramr.Core.Interfaces.Data.Services;
using Gramr.Core.Models.Data;

namespace Gramr.Logic.Services
{
    public class MarketDataManagementService : IMarketDataManagementService
    {
        private readonly IDataService<MarketAggregate> _dbService;
        private readonly IMarketDataRetrievalService _apiService;

        public MarketDataManagementService(IDataService<MarketAggregate> dbService,
            IMarketDataRetrievalService apiService)
        {
            _dbService = dbService;
            _apiService = apiService;
        }

        public async Task<List<MarketAggregate>> GetData(Company? company = null)
        {
            if (company == null)
                return new List<MarketAggregate>();

            var earliestDate = DateTime.UtcNow.Date.AddYears(-1);
            var dbData = await GetDbData(company, earliestDate);
            var mostRecentDbDate = dbData.MaxBy(d => d.Timestamp)?.Timestamp;

            //If market data is up to date, simply return it
            if (mostRecentDbDate != null && mostRecentDbDate.Value >= DateTime.UtcNow.Date)
                return dbData;

            //If not, we need to retrieve it and save it
            var startDate = mostRecentDbDate?.AddMinutes(1) ?? earliestDate;
            var apiData = await UpdateDbData(company, startDate, DateTime.UtcNow);

            //And then return it
            dbData.AddRange(apiData);
            return dbData;
        }

        private async Task<List<MarketAggregate>> GetDbData(Company company, DateTime sinceDate)
        {
            return (await _dbService.QueryAsync(d => d.CompanyId == company.Id && d.Timestamp > sinceDate)).ToList();
        }

        private async Task<List<MarketAggregate>> UpdateDbData(Company company, DateTime startDate, DateTime endDate)
        {
            if (!company.Active)
                return new List<MarketAggregate>();

            var apiData = await _apiService.GetAggregates(company, startDate, endDate);
            await _dbService.CreateManyAsync(apiData);

            return apiData;
        }
    }
}