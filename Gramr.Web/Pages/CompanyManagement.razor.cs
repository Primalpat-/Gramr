using Gramr.Core.Interfaces.Data.Services;
using Gramr.Core.Models.Data;
using Microsoft.AspNetCore.Components;

namespace Gramr.Web.Pages
{
    public partial class CompanyManagement
    {
        [Inject]
        private IDataService<Company> DataService { get; set; } = default!;

        private List<Company>? Companies { get; set; } = default!;

        private Company? _selectedCompany;

        protected override async Task OnInitializedAsync()
        {
            Companies = (await DataService.QueryAsync()).OrderBy(c => c.Ticker).ToList();
        }

        private void CompanySelected(ChangeEventArgs e)
        {
            _selectedCompany = Companies?.FirstOrDefault(c => c.Id == e.Value?.ToString()) ?? new();
        }

        private async Task CreateOrUpdateCompany(Company newOrChangedCompany)
        {
            if (newOrChangedCompany.Id?.Length > 0)
                await DataService.UpdateAsync(newOrChangedCompany);
            else
            {
                await DataService.CreateAsync(newOrChangedCompany);
                Companies.Add(newOrChangedCompany);
            }
        }

        private async Task DeleteCompany(Company deletedCompany)
        {
            await DataService.DeleteAsync(deletedCompany.Id);
            Companies.Remove(deletedCompany);
        }
    }
}
