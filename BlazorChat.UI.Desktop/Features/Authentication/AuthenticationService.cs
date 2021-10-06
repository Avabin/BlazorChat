using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using BlazorChat.UI.Shared.Features.Authentication;
using BlazorChat.UI.Shared.Features.Authentication.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace BlazorChat.UI.Desktop.Features.Authentication
{
    public class AuthenticationService : AuthenticationServiceBase
    {
        private readonly Lazy<IPublicClientApplication> _clientLazy;
        private IPublicClientApplication Client => _clientLazy.Value;
        private IOptions<AuthenticationSettings> _settings;

        public AuthenticationService(ILogger<AuthenticationService> logger, IOptions<AuthenticationSettings> settings) : base(logger)
        {
            _settings = settings;
            _clientLazy = new Lazy<IPublicClientApplication>(() 
                => PublicClientApplicationBuilder
                    .Create(_settings.Value.ClientId)
                    .WithRedirectUri(_settings.Value.RedirectUrl)
                    .WithAuthority(AzureCloudInstance.AzurePublic, _settings.Value.TenantId)
                    .Build());
        }

        protected override IObservable<AuthenticationData> AuthenticateInner(IEnumerable<string> scopes)  =>
            Client.AcquireTokenInteractive(scopes)
                .ExecuteAsync()
                .ToObservable<AuthenticationResult>()
                .Select(AuthenticationData.FromMicrosoftAuthenticationResult);
    }
}