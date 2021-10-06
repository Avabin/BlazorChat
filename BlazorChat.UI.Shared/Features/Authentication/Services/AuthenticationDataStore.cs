using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Identity.Client;
using ReactiveUI;

namespace BlazorChat.UI.Shared.Features.Authentication.Services
{
    public class AuthenticationDataStore
    {
        private ISubject<AuthenticationData> _authSubject;

        public IObservable<AuthenticationData> AuthorizationResultObservable =>
            _authSubject.AsObservable().WhereNotNull();

        public AuthenticationDataStore()
        {
            _authSubject = new BehaviorSubject<AuthenticationData>(null!);
        }

        public void SetAuthenticationData(AuthenticationData authenticationData) => _authSubject.OnNext(authenticationData);
    }
}