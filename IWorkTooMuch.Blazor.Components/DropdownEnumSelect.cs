using SEG.Components.Blazor.Extensions;
using System;

namespace SEG.Components.Blazor
{
    public class DropdownEnumSelect<T> : DropdownSelect<T>
        where T : Enum
    {
        protected override string GetDisplayValue(T option) => option.GetDisplayName();
    }
}
