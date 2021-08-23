using Microsoft.AspNetCore.Components;

namespace IWorkTooMuch.Blazor.Components.DataGridSupporters.Filters
{
    public partial class FilterMenuWrapper
    {
        [Parameter]
        public EventCallback AcceptClicked { get; set; }

        [Parameter]
        public EventCallback ClearClicked { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string ColumnName { get; set; }
    }
}
