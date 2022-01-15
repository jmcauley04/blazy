using Microsoft.Extensions.DependencyInjection;

namespace Blazy.Services.JS
{
    public static class ConfigureJS
    {
        public static IServiceCollection AddJS(this IServiceCollection services)
        {
            services.AddTransient<ClipboardService>();
            services.AddTransient<ScrollToService>();
            services.AddTransient<ElementCoordinatesService>();
            return services;
        }
    }    
}
