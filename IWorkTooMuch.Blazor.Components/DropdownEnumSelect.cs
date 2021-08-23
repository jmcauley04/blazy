using IWorkTooMuch.Blazor.Components.Extensions;
using System;

namespace IWorkTooMuch.Blazor.Components
{
    public class DropdownEnumSelect<T> : DropdownSelect<T>
        where T : Enum
    {
        protected override string GetDisplayValue(T option) => option.GetDisplayName();
    }
}
