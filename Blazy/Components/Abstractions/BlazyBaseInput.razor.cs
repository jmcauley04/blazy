using Microsoft.AspNetCore.Components;

namespace Blazy.Components.Abstractions
{
    public abstract partial class BlazyBaseInput<TItem>
    {
        protected int? _min;
        protected int? _max;
        protected bool isValidated = false;
        string placeholder = string.Empty;
        string focusClass = string.Empty;
        protected bool isEmpty = true;
        protected virtual string smallClass => isEmpty && string.IsNullOrEmpty(focusClass) ? "" : "small";
        string readonlyClass => Readonly ? "readonly" : "";
        Guid guid = Guid.NewGuid();

        [Parameter]
        public string Label { get; set; } = string.Empty;

        [Parameter]
        public string Placeholder { get; set; } = string.Empty;

        [Parameter]
        public TItem? Value { get; set; } = default;

        [Parameter]
        public EventCallback<TItem> ValueChanged { get; set; }

        [Parameter]
        public Func<bool>? IsValid { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool Masked { get; set; }

        [Parameter]
        public EventCallback OnClick { get; set; }

        protected virtual string? _typeOverride { get; }

        protected abstract void OnChange(ChangeEventArgs args);

        private void SetFocus()
        {
            if (!Readonly)
            {
                placeholder = Placeholder;
                focusClass = "focused";
            }
        }

        private void UnsetFocus()
        {
            placeholder = string.Empty;
            focusClass = string.Empty;
        }
    }
}
