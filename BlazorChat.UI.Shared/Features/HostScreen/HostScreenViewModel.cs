using System;
using System.Reactive;
using BlazorChat.UI.Shared.Features.Counter;
using BlazorChat.UI.Shared.Features.Navigation;
using BlazorChat.UI.Shared.Features.WeatherForecast;
using ReactiveUI;

namespace BlazorChat.UI.Shared.Features.HostScreen
{
    public class HostScreenViewModel : ReactiveObject, IScreen
    {
        private readonly INavigationService _navigationService;
        public ReactiveCommand<Unit, IRoutableViewModel> NavigateForecastCommand { get; set; }
        public ReactiveCommand<Unit, IRoutableViewModel> NavigateCounterCommand { get; set; }
        public RoutingState Router => _navigationService.Router;
        public HostScreenViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateForecastCommand = ReactiveCommand.CreateFromObservable(NavigateToForecast);
            NavigateCounterCommand = ReactiveCommand.CreateFromObservable(NavigateToCounter);
        }

        private IObservable<IRoutableViewModel> NavigateToForecast() => _navigationService.NavigateTo<WeatherForecastViewModel>();
        private IObservable<IRoutableViewModel> NavigateToCounter() => _navigationService.NavigateTo<CounterViewModel>();
    }
}