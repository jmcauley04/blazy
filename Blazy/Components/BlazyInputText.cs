using Blazy.Components.Abstractions;
using Microsoft.AspNetCore.Components;

namespace Blazy.Components
{
    public class BlazyInputText : BlazyBaseInput<string>
    {
        protected override string? _typeOverride => Masked ? "password" : "text";

        protected override void OnInitialized()
        {
            Validate(Value);
            base.OnInitialized();
        }

        protected override void OnChange(ChangeEventArgs args)
        {
            var newValue = args.Value?.ToString();
            Validate(newValue);
        }

        void Validate(string? value)
        {
            isEmpty = string.IsNullOrEmpty(value);

            ValueChanged.InvokeAsync(value);
            if (Value != value)
                Value = value ?? string.Empty;

            isValidated = (IsValid != null && IsValid.Invoke()) || (IsValid == null && !string.IsNullOrEmpty(Value));
        }
    }
}
