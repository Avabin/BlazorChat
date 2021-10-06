using System;
using BlazorChat.UI.Shared.Features.Authentication;
using BlazorChat.UI.Shared.Features.Authentication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BlazorChat.UI.Desktop.Features.Authentication
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection s, IConfiguration configuration)
            => s.Configure<AuthenticationSettings>(configuration.GetSection("AuthenticationSettings"))
                .AddTransient<IValidateOptions<AuthenticationSettings>, AuthenticationSettingsValidator>()
                .AddSingleton<AuthenticationDataStore>()
                .AddSingleton<IAuthenticationService, AuthenticationService>();
        
        public static IServiceCollection AddAuthentication(this IServiceCollection s, Action<AuthenticationSettings> configure)
            => s.Configure(configure)
                .AddTransient<IValidateOptions<AuthenticationSettings>, AuthenticationSettingsValidator>()
                .AddSingleton<AuthenticationDataStore>()
                .AddSingleton<IAuthenticationService, AuthenticationService>();
    }
}