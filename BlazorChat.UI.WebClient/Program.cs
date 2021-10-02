using System;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blazor.Extensions.Logging;
using BlazorChat.UI.Shared;
using BlazorChat.UI.Shared.Features.Navigation;
using BlazorChat.UI.WebClient.Features;
using BlazorChat.UI.WebClient.Features.Navigation;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using Splat;
using Splat.Autofac;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace BlazorChat.UI.WebClient
{
    public class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.Services
                .AddBlazorise( options =>
                {
                    options.ChangeTextOnKeyPress = false;
                } )
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
            
            var resolver = Locator.CurrentMutable;
            resolver.InitializeSplat();
            resolver.InitializeReactiveUI();
            
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("User.Read");
            });

            builder.Services.AddNavigation<BlazorNavigationService>();

            builder.ConfigureContainer(new AutofacServiceProviderFactory(ConfigureContainer));
            
            builder.Services.AddLogging(b => b
                .AddBrowserConsole()
                .SetMinimumLevel(LogLevel.Trace)
            );
            
            var host =  builder.Build();
            ServiceProvider = host.Services;
            await host.RunAsync();
        }

        private static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.UseAutofacDependencyResolver();
            builder.RegisterModule(new AutofacModule(typeof(Program).Assembly, typeof(AutofacModule).Assembly));
        }
    }
}
