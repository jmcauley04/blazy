using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;

namespace Blazy.Extensions
{
    public static class JsRuntimeExtensions
    {
        public static async Task<IJSObjectReference> Import(
            this IJSRuntime jsRuntime,
            string wwwrootpath)
        {
            var module = await jsRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                Path.Combine($"./_content/Blazy/", wwwrootpath)
            );
            return module;
        }

        public static async Task<IJSObjectReference> Import<T>(
            this IJSRuntime jsRuntime,
            T instance)
        {
            var path = instance!.GetType().FullName!.Replace('.', '/');
            var module = await jsRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                Path.Combine($"./_content/", $"{path}.razor.js")
            );
            return module;            
        }
    }
}
