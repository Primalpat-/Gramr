using System.Net;
using System.Net.Http.Headers;
using Gramr.Core.Interfaces.Api;
using Gramr.Core.Models.Data;
using Gramr.Core.Models.Dtos;
using Gramr.Logic.Extensions;
using Microsoft.Extensions.Options;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Gramr.Logic.Services.Api
{
    public class MarketDataRetrievalService : IMarketDataRetrievalService
    {
        private readonly IOptions<ApiSettings> _settings;

        public MarketDataRetrievalService(IOptions<ApiSettings> settings)
        {
            _settings = settings;
        }

        public async Task<List<MarketAggregate>> GetAggregates(Company company, DateTime start, DateTime end)
        {
            var results = new List<MarketAggregate>();
            var aggregateData = new AggregateDataDto();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_settings.Value.BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.Value.Token);
                var response = await client.GetAsync($"v2/aggs/ticker/{company.Ticker}/range/1/minute/{start.ToUnixMs()}/{end.ToUnixMs()}?adjusted=true&sort=asc&limit=50000");
                if (response.IsSuccessStatusCode)
                {
                    var resultData = response.Content.ReadAsStringAsync().Result;
                    aggregateData = JsonConvert.DeserializeObject<AggregateDataDto>(resultData);
                }
            }

            foreach (var dto in aggregateData.results)
                results.Add(new MarketAggregate()
                {
                    CompanyId = company.Id,
                    Timestamp = dto.t.ToDateTime(),
                    Transactions = dto.n,
                    Volume = dto.v,
                    Open = dto.o,
                    Close = dto.c,
                    High = dto.h,
                    Low = dto.l,
                    Average = dto.vw
                });

            return results;
        }
    }
}
