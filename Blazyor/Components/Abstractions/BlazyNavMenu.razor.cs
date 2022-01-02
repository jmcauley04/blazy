using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Blazyor.Components.Abstractions
{
    public partial class BlazyNavMenu
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

            public bool IsOpen { get; set; } = false;
        }

        string sublinkStyle(BlazyNavLink navLink) => $"max-height: {52 * countOpenSublinks(navLink)}px;";

        int countOpenSublinks(BlazyNavLink link)
        {
            int i = 0;

            if (link.IsOpen)
            {
                foreach (var sublink in link.Sublinks!.Where(x => x.IsOpen))
                    i += countOpenSublinks(sublink);

                i += link.Sublinks!.Count();
            }
            else
                return 0;

            return i;
        }
    }
}
