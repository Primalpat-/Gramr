using Gramr.Web.Components.Widgets;

namespace Gramr.Web.Pages
{
    public partial class Index
    {
        public List<Type> Widgets { get; set; } = new List<Type>()
        {
            typeof(EmployeeCountWidget),
            typeof(InboxWidget)
        };
    }
}
