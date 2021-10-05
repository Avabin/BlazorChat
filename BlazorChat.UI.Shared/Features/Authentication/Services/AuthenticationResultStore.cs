using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Identity.Client;
using ReactiveUI;

namespace BlazorChat.UI.Shared.Features.Authentication.Services
{
    public class AuthenticationResultStore
    {
        private ISubject<AuthenticationResult> _authSubject;

        public IObservable<AuthenticationResult> AuthorizationResultObservable =>
            _authSubject.AsObservable().WhereNotNull();

        public AuthenticationResultStore()
        {
            _authSubject = new BehaviorSubject<AuthenticationResult>(null!);
        }

        public void SetAuthenticationResult(AuthenticationResult authenticationResult) => _authSubject.OnNext(authenticationResult);
    }
}