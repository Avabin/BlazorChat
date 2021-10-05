using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace BlazorChat.UI.Shared.Features.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Lazy<IPublicClientApplication> _clientLazy;
        private IPublicClientApplication Client => _clientLazy.Value;
        private readonly ISubject<AuthenticationStatus> _authenticationStatusSubject;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IOptions<AuthenticationSettings> _settings;

        public AuthenticationService(ILogger<AuthenticationService> logger, IOptions<AuthenticationSettings> settings)
        {
            _logger = logger;
            _settings = settings;
            _authenticationStatusSubject = new BehaviorSubject<AuthenticationStatus>(AuthenticationStatus.NotAuthenticated);

            _clientLazy = new Lazy<IPublicClientApplication>(() 
                => PublicClientApplicationBuilder
                    .Create(_settings.Value.ClientId)
                    .WithRedirectUri(_settings.Value.RedirectUrl)
                    .WithAuthority(AzureCloudInstance.AzurePublic, _settings.Value.TenantId)
                    .Build());
        }

        /// <inheritdoc />
        public IObservable<AuthenticationStatus> AuthenticationStatusObservable =>
            _authenticationStatusSubject
                .AsObservable()
                .Do(s => _logger.LogDebug("AuthenticationStatus changed to {AuthenticationStatus}", s));

        /// <inheritdoc />
        public IObservable<bool> IsAuthenticatedObservable =>
            AuthenticationStatusObservable
                .Select(x => x == AuthenticationStatus.Authenticated);


        /// <inheritdoc />
        public IObservable<AuthenticationResult> Authenticate(IEnumerable<string> scopes) =>
            Client.AcquireTokenInteractive(scopes)
                .ExecuteAsync()
                .ToObservable()
                .Do(_ => _authenticationStatusSubject.OnNext(AuthenticationStatus.Authenticated));

        /// <inheritdoc />
        public IObservable<AuthenticationResult> Authenticate() => Authenticate(new[] { "User.Read" });
    }
}