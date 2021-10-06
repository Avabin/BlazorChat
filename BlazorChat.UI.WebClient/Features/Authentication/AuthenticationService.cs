using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Blazorade.Msal.Security;
using Blazorade.Msal.Services;
using BlazorChat.UI.Shared.Features.Authentication;
using BlazorChat.UI.Shared.Features.Authentication.Services;
using Microsoft.Extensions.Logging;

namespace BlazorChat.UI.WebClient.Features.Authentication
{
    public class AuthenticationService : AuthenticationServiceBase
    {
        private readonly BlazoradeMsalService _msalService;

        public AuthenticationService(ILogger<AuthenticationService> logger, BlazoradeMsalService msalService) : base(logger)
        {
            _msalService = msalService;
        }

        protected override IObservable<AuthenticationData> AuthenticateInner(IEnumerable<string> scopes) =>
            _msalService.AcquireTokenAsync(new TokenAcquisitionRequest
                {
                    Scopes = scopes,
                })
                .ToObservable()
                .Select(MapToAuthenticationData);

        private static AuthenticationData MapToAuthenticationData(AuthenticationResult ar) 
            => new(ar.UniqueId, ar.TokenType, ar.AccessToken, ar.Scopes, ar.ExpiresOn, new AuthAccount(ar.Account));
    }
}