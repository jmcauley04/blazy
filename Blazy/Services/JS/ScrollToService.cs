using Microsoft.JSInterop;

namespace Blazy.Services.JS
{
    public class ScrollToService : BaseJsModule
    {
        public ScrollToService(IJSRuntime jSRuntime) : base("ScrollToService.js", jSRuntime)
        {
        }

        public ValueTask ScrollToY(Microsoft.AspNetCore.Components.ElementReference element)
        {
            if (module is not null)            
                module.InvokeVoidAsync("ScrollToY", element);

            throw NullModuleException();
        }
    }
}
