using System;
using System.Reactive;
using BlazorChat.UI.Shared.Features.Navigation;
using BlazorChat.UI.Shared.Features.WeatherForecast;
using ReactiveUI;

namespace BlazorChat.UI.Shared.Features.Index
{
    public class HostScreenViewModel : ReactiveObject, IScreen
    {
        private readonly INavigationService _navigationService;
        public ReactiveCommand<Unit, IRoutableViewModel> NavigateCommand { get; set; }
        public RoutingState Router => _navigationService.Router;
        public HostScreenViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = ReactiveCommand.CreateFromObservable(Navigate);
        }

        private IObservable<IRoutableViewModel> Navigate() => _navigationService.NavigateTo<WeatherForecastViewModel>();
    }
}