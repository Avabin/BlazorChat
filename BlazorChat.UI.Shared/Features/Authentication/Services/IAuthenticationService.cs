using System;
using System.Collections.Generic;
using Microsoft.Identity.Client;

namespace BlazorChat.UI.Shared.Features.Authentication.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Emits current <see cref="AuthenticationStatus"/> when authentication status changes
        /// Last emitted element is always available for new observers
        /// </summary>
        IObservable<AuthenticationStatus> AuthenticationStatusObservable { get; }
        
        /// <summary>
        /// Emits true if user is currently authenticated
        /// Last emitted element is always available for new observers
        /// </summary>
        IObservable<bool> IsAuthenticatedObservable { get; }
        
        /// <summary>
        /// Create authentication observable
        /// </summary>
        /// <param name="scopes">Authentication token scopes</param>
        /// <returns>
        /// Authentication observable which emits
        /// single element, the result of authentication
        /// when authentication completes
        /// <see cref="AuthenticationResult"/>
        /// </returns>
        IObservable<AuthenticationResult> Authenticate(IEnumerable<string> scopes);

        /// <summary>
        /// Create authentication observable with default scope "User.Read"
        /// </summary>
        /// <returns>
        /// Authentication observable which emits
        /// single element, the result of authentication
        /// when authentication completes
        /// <see cref="AuthenticationResult"/>
        /// </returns>
        IObservable<AuthenticationResult> Authenticate();
    }
}