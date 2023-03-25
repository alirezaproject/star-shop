using System.Globalization;
using System.Reflection;
using System.Reflection.Metadata;
using Blazored.LocalStorage;
using Infrastructure.Client.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace EndPoint.Client.Extensions
{
    public static class WebAssemblyHostBuilderExtensions
    {
        private const string ClientName = "BlazorHero.API";

        public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#app");

            return builder;
        }

        public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
        {
            builder
                .Services
                .AddBlazoredLocalStorage()
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                //  .AddScoped<BlazorHeroStateProvider>()
                //  .AddScoped<AuthenticationStateProvider, BlazorHeroStateProvider>()
                .AddManagers()
                //.AddTransient<AuthenticationHeaderHandler>()
                ;

            return builder;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            var managers = typeof(IManager);

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                {
                    services.AddScoped(type.Service, type.Implementation);
                }
            }

            return services;
        }
    }
}