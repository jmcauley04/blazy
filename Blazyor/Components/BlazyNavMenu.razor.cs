using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Blazyor.Components
{
    public partial class BlazyNavMenu
    {
        [Parameter]
        public IEnumerable<BlazyNavLink> Links { get; set; }

        public class BlazyNavLink
        {
            public string Text { get; set; } = string.Empty;

            public string? Href { get; set; }

            public IEnumerable<BlazyNavLink>? Sublinks { get; set; }

            public NavLinkMatch LinkMatch { get; set; } = NavLinkMatch.All;
        }
    }
}
