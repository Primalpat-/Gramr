using Microsoft.AspNetCore.Components;

namespace Gramr.Web.Components
{
    public partial class ProfilePicture
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}