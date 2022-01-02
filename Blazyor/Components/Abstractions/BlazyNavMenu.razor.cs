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

            public string SublinksStyle { get; set; } = string.Empty;
        }

        void SetSublinkStyle(BlazyNavLink navlink)
        {
            if (string.IsNullOrEmpty(navlink.SublinksStyle))
                navlink.SublinksStyle = $"max-height: {52 * navlink.Sublinks!.Count()}px;";
            else
                navlink.SublinksStyle = string.Empty;
        }
    }
}
