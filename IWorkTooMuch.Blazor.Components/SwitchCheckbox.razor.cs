using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace IWorkTooMuch.Blazor.Components
{
    public enum SwitchShape
    {
        Default,
        Round,
        Square
    }
    public partial class SwitchCheckbox
    {
        [Parameter]
        public SwitchShape Shape { get; set; } = SwitchShape.Default;

        private bool _checked;
        [Parameter]
        public bool Checked { get => _checked; set => _checked = value; }

        [Parameter]
        public EventCallback<bool> CheckedChanged { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        private string SwitchMode => Shape switch
        {
            SwitchShape.Round => "slider-round",
            SwitchShape.Square => "slider-square",
            _ => ""
        };

        private async Task UpdateCheckedFromChild()
        {
            if (!Disabled)
                await CheckedChanged.InvokeAsync(!Checked);
        }
    }
}
