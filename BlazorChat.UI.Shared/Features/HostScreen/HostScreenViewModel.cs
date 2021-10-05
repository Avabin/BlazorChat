using System;
using System.Reactive;
using BlazorChat.UI.Shared.Features.Authentication;
using BlazorChat.UI.Shared.Features.Chat;
using BlazorChat.UI.Shared.Features.Counter;
using BlazorChat.UI.Shared.Features.Navigation;
using BlazorChat.UI.Shared.Features.WeatherForecast;
using ReactiveUI;

namespace BlazorChat.UI.Shared.Features.HostScreen
{
    public class HostScreenViewModel : ReactiveObject, IScreen
    {
        private readonly INavigationService _navigationService;
        public ReactiveCommand<Unit, IRoutableViewModel> NavigateForecastCommand { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> NavigateCounterCommand { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> NavigateChatCommand { get; private set; }
        public ReactiveCommand<Unit, IRoutableViewModel> NavigateLoginCommand { get; private set; }
        public RoutingState Router => _navigationService.Router;
        
        public HostScreenViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
            NavigateForecastCommand = ReactiveCommand.CreateFromObservable(NavigateToForecast);
            NavigateCounterCommand = ReactiveCommand.CreateFromObservable(NavigateToCounter);
            NavigateChatCommand = ReactiveCommand.CreateFromObservable(NavigateToChat);
            NavigateLoginCommand = ReactiveCommand.CreateFromObservable(NavigateToLogin);
        }

        private IObservable<IRoutableViewModel> NavigateToForecast() => _navigationService.NavigateTo<WeatherForecastViewModel>();
        private IObservable<IRoutableViewModel> NavigateToCounter() => _navigationService.NavigateTo<CounterViewModel>();
        private IObservable<IRoutableViewModel> NavigateToChat() => _navigationService.NavigateTo<ChatViewModel>();
        private IObservable<IRoutableViewModel> NavigateToLogin() => _navigationService.NavigateTo<AuthenticationViewModel>();
    }
}