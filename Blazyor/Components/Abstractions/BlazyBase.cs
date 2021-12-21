using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazyor.Components.Abstractions
{
    public class BlazyBase : ComponentBase
    {
        [Parameter]
        public string CssClass { get; set; }

        [Parameter]
        public string CssStyle { get; set; }
    }
}
