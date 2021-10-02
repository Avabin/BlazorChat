using System;
using BlazorChat.UI.Shared.Features.Navigation;
using BlazorChat.UI.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Splat;

namespace BlazorChat.UI.Desktop
{
    public class Startup
    {
        private readonly IHostEnvironment _environment;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(MessageBus.Current);
            services.AddSingleton(_environment.ContentRootFileProvider);

            services.AddNavigation();

            Locator.CurrentMutable.InitializeSplat();
            Locator.CurrentMutable.InitializeReactiveUI();

            Locator.CurrentMutable.RegisterLazySingleton(() 
                => new AutofacViewLocator(
                    new Lazy<IServiceProvider>(App.Container)),
                typeof(IViewLocator));
        }
    }
}