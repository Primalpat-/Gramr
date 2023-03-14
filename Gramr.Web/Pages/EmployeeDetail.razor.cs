using BethanysPieShopHRM.Shared.Domain;
using BethanysPieShopHRM.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Gramr.Web.Pages
{
    public partial class EmployeeDetail
    {
        [Parameter]
        public string EmployeeId { get; set; }

        public Employee? Employee { get; set; } = new();

        protected override Task OnInitializedAsync()
        {
            Employee = MockDataService.Employees.FirstOrDefault(e => e.EmployeeId == int.Parse(EmployeeId));
            return base.OnInitializedAsync();
        }
    }
}
