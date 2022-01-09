using Microsoft.AspNetCore.Components;

namespace Blazy.Components.Abstractions
{
    public class BlazyBase : ComponentBase
    {
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        [Parameter]
        public string CssStyle { get; set; } = string.Empty;
    }
}
