using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace SEG.Components.Blazor
{
    public partial class DropdownSelect<T>
    {
        [Parameter]
        public List<T> Options { get; set; }

        [Parameter]
        public T SelectedValue { get => Options[OptionIndex]; set => OptionIndex = Options.IndexOf(value); }

        [Parameter]
        public EventCallback<T> SelectedValueChanged { get; set; }

        private int optionIndex;
        private int OptionIndex { get => optionIndex; set { if (optionIndex != value) { optionIndex = value; SelectedValueChanged.InvokeAsync(SelectedValue); } } }

        protected virtual string GetDisplayValue(T option) => option.ToString();
    }
}
