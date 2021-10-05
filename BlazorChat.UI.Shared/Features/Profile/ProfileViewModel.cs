using System;
using System.Reactive.Linq;
using BlazorChat.UI.Shared.Features.Authentication.Services;
using BlazorChat.UI.Shared.Features.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BlazorChat.UI.Shared.Features.Profile
{
    public class ProfileViewModel : RoutableViewModel
    {
        private readonly AuthenticationResultStore _authStore;
        
        [Reactive] public string Username { get; set; } = "";
        
        public ProfileViewModel(IScreen hostScreen, AuthenticationResultStore authStore) : base(hostScreen)
        {
            _authStore = authStore;

            _authStore.AuthorizationResultObservable
                .Do(x => Username = x.Account.Username)
                .Subscribe();
        }

        public override string? UrlPathSegment => "profile";
    }
}