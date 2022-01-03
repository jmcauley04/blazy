using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;

namespace Blazy.Extensions
{
    internal static class JsRuntimeExtensions
    {
        internal static async Task<IJSObjectReference> Import(
            this IJSRuntime jsRuntime,
            string pathFromWwwRoot)
        {
            var module = await jsRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                Path.Combine($"./_content/Blazy/", pathFromWwwRoot)
            );
            return module;
        }
    }
}
