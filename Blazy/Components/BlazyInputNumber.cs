using Blazy.Components.Abstractions;
using Microsoft.AspNetCore.Components;

namespace Blazy.Components
{
    public class BlazyInputNumber : BlazyBaseInput<int>
    {
        protected override string? _typeOverride => "number";

        [Parameter]
        public int? Max { get => _max; set => _max = value; }

        [Parameter]
        public int? Min { get => _min; set => _min = value; }

        protected override void OnInitialized()
        {
            Validate(Value);
            base.OnInitialized();
        }

        protected override void OnChange(ChangeEventArgs args)
        {
            if (int.TryParse(args.Value?.ToString(), out int intValue))
                Validate(intValue);
            else
                Validate(null);
        }

        void Validate(int? intValue)
        {
            isEmpty = intValue == null;

            ValueChanged.InvokeAsync(intValue ?? 0);
            if (Value != intValue)
                Value = intValue ?? 0;            

            isValidated = (IsValid != null && IsValid.Invoke()) || (IsValid == null && !string.IsNullOrEmpty(intValue.ToString()));
        }
    }
}
