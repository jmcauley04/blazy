using Microsoft.JSInterop;

namespace Blazy.Services.JS
{
    public class ElementCoordinatesService : BaseJsModule
    {
        public ElementCoordinatesService(IJSRuntime jSRuntime) : base("ElementCoordinatesService.js", jSRuntime)
        {
        }

        public ValueTask<BoundingClientRect> GetCoordinates(Microsoft.AspNetCore.Components.ElementReference element)
        {
            if (module is not null)
                return module.InvokeAsync<BoundingClientRect>("GetCoordinates", element);

            throw NullModuleException();
        }
        public class BoundingClientRect
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public double Top { get; set; }
            public double Right { get; set; }
            public double Bottom { get; set; }
            public double Left { get; set; }
        }
    }
}
