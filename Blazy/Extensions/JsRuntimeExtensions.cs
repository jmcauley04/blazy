using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;

namespace Blazy.Extensions
{
    internal static class JsRuntimeExtensions
    {
        internal static async Task<IJSObjectReference> Import(
            this IJSRuntime jsRuntime,
            string path)
        {
            var module = await jsRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                Path.Combine($"./_content/Blazy/", path)
            );
            return module;
        }

        internal static async Task<IJSObjectReference> Import<T>(
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
