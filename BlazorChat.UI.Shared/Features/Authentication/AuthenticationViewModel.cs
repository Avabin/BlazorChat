using System;
using System.Reactive;
using System.Reactive.Linq;
using BlazorChat.UI.Shared.Features.Authentication.Services;
using BlazorChat.UI.Shared.Features.Navigation;
using BlazorChat.UI.Shared.Features.Profile;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace BlazorChat.UI.Shared.Features.Authentication
{
    public class AuthenticationViewModel : RoutableViewModel
    {
        private readonly IAuthenticationService _service;
        private readonly AuthenticationDataStore _authStore;
        private readonly INavigationService _navigationService;
        public ReactiveCommand<Unit, Unit> AuthenticateCommand { get; set; }

        public AuthenticationViewModel(IScreen hostScreen, 
            IAuthenticationService service,
            AuthenticationDataStore authStore, 
            ILogger<AuthenticationViewModel> logger,
            INavigationService navigationService)
        : base(hostScreen)
        {
            _service = service;
            _authStore = authStore;
            _navigationService = navigationService;

            AuthenticateCommand = ReactiveCommand.CreateFromObservable(Authenticate)!;

            AuthenticateCommand
                .ThrownExceptions
                .Do(ex => logger.LogError(ex, "An error occured during authentication!"))
                .Subscribe();
        }

        private IObservable<Unit> Authenticate() =>
            _service.Authenticate()
                .Do(_authStore.SetAuthenticationData)
                // Map to void
                .Select(_ => Unit.Default);

        public override string UrlPathSegment => "authenticate";
    }
}