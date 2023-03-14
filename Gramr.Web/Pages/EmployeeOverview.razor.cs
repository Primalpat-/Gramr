using BethanysPieShopHRM.Shared.Domain;
using BethanysPieShopHRM.Shared.Models;

namespace Gramr.Web.Pages
{
    public partial class EmployeeOverview
    {
        public List<Employee>? Employees { get; set; } = default!;
        private Employee? _selectedEmployee;

        protected override void OnInitialized()
        {
            Employees = MockDataService.Employees;
        }

        public void ShowQuickViewPopup(Employee selectedEmployee)
        {
            _selectedEmployee = selectedEmployee;
        }
    }
}
