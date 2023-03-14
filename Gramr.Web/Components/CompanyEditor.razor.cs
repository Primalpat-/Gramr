using Gramr.Core.Interfaces.Data.Services;
using Gramr.Core.Models.Data;
using Microsoft.AspNetCore.Components;

namespace Gramr.Web.Components
{
    public partial class CompanyEditor
    {
        [Inject]
        private IDataService<Company> DataService { get; set; } = default!;

        [Inject]
        private IMarketDataManagementService MarketService { get; set; } = default!;

        [Parameter]
        public Company? Company { get; set; }

        [Parameter]
        public EventCallback<Company> CompanyFormSubmitted { get; set; }

        [Parameter]
        public EventCallback<Company> CompanyFormDeleted { get; set; }

        private Company? _company;
        private List<MarketAggregate>? _aggregates;
        private bool _loadingMarketData = false;

        protected override void OnParametersSet()
        {
            _company = Company;
            _aggregates = null;
        }

        protected override async Task OnParametersSetAsync()
        {
            await RefreshMarketData();
        }

        public void CancelOrDelete()
        {
            if (_company.Id?.Length > 0)
                CompanyFormDeleted.InvokeAsync(_company);

            _company = null;
        }

        private async Task SubmitAsync()
        {
            if (Company?.Id?.Length > 0)
            {
                await DataService.UpdateAsync(_company);
            }
            else
            {
                await DataService.CreateAsync(_company);
            }
        }

        private async Task RefreshMarketData()
        {
            _loadingMarketData = true;
            _aggregates = await MarketService.GetData(_company);
            _loadingMarketData = false;
        }
    }
}