using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Blazy.Components.Abstractions
{
    public partial class BlazyNavMenu : ComponentBase
    {
        [Parameter]
        public IEnumerable<BlazyNavLink> Links { get; set; } = new List<BlazyNavLink>();

        public class BlazyNavLink
        {
            public string Text { get; set; } = string.Empty;

            public string? Href { get; set; }

            public Action? OnClick { get; set; }

            public IEnumerable<BlazyNavLink>? Sublinks { get; set; }

            public NavLinkMatch LinkMatch { get; set; } = NavLinkMatch.All;

            internal bool IsOpen { get; set; } = false;
        }

    }
}
