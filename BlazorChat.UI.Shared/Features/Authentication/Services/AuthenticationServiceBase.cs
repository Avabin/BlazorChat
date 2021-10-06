using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace BlazorChat.UI.Shared.Features.Authentication.Services
{
    public abstract class AuthenticationServiceBase : IAuthenticationService
    {
        protected ILogger<AuthenticationServiceBase> Logger { get; }
        
        protected BehaviorSubject<AuthenticationStatus> AuthenticationStatusSubject { get; }

        protected AuthenticationServiceBase(ILogger<AuthenticationServiceBase> logger)
        {
            Logger = logger;
            AuthenticationStatusSubject = new BehaviorSubject<AuthenticationStatus>(AuthenticationStatus.NotAuthenticated);
        }

        /// <inheritdoc />
        public IObservable<AuthenticationStatus> AuthenticationStatusObservable =>
            AuthenticationStatusSubject.AsObservable()
                .Do(s => Logger.LogDebug("AuthenticationStatus changed to {AuthenticationStatus}", s));

        /// <inheritdoc />
        public IObservable<bool> IsAuthenticatedObservable =>
            AuthenticationStatusObservable
                .Select(x => x == AuthenticationStatus.Authenticated);

        public IObservable<AuthenticationData> Authenticate(IEnumerable<string> scopes) => AuthenticateInner(scopes)
            .Do(_ => AuthenticationStatusSubject.OnNext(AuthenticationStatus.Authenticated));

        public IObservable<AuthenticationData> Authenticate() => Authenticate(new[] { "User.Read" });
        protected abstract IObservable<AuthenticationData> AuthenticateInner(IEnumerable<string> scopes);
    }
}