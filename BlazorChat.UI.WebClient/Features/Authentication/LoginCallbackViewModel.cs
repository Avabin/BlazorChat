using System;
using System.Reactive;
using Blazorade.Msal.Security;
using BlazorChat.UI.Shared.Features.Authentication;
using BlazorChat.UI.Shared.Features.Authentication.Services;
using BlazorChat.UI.Shared.Features.Navigation;
using BlazorChat.UI.Shared.Features.Profile;
using ReactiveUI;

namespace BlazorChat.UI.WebClient.Features.Authentication
{
    public class LoginCallbackViewModel : ReactiveObject
    {
        private readonly AuthenticationDataStore _store;
        private readonly INavigationService _navigationService;
        public ReactiveCommand<AuthenticationResult, IObservable<IRoutableViewModel>> SaveAuthResultAndNavigateToProfileCommand { get; }

        public LoginCallbackViewModel(AuthenticationDataStore store, INavigationService navigationService)
        {
            _store = store;
            _navigationService = navigationService;
            
            SaveAuthResultAndNavigateToProfileCommand = ReactiveCommand.Create<AuthenticationResult, IObservable<IRoutableViewModel>>(Execute);
        }

        private IObservable<IRoutableViewModel> Execute(AuthenticationResult ar)
        {
            _store.SetAuthenticationData(new AuthenticationData(ar.UniqueId, ar.TokenType, ar.AccessToken, ar.Scopes, ar.ExpiresOn, new AuthAccount(ar.Account)));
            return _navigationService.NavigateTo<ProfileViewModel>();
        }
    }
}