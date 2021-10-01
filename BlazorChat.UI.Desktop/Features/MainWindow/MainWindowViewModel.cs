using System;
using System.Reactive;
using BlazorChat.UI.Shared;
using BlazorChat.UI.Shared.Features.Counter;
using BlazorChat.UI.Shared.Features.Navigation;
using BlazorChat.UI.Shared.Features.WeatherForecast;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BlazorChat.UI.Desktop.Features.MainWindow
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        private readonly INavigationService _navigationService;
        public ReactiveCommand<Unit, IRoutableViewModel> NavigateCommand { get; set; }
        public RoutingState Router => _navigationService.Router;
        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = ReactiveCommand.CreateFromObservable(Navigate);
        }

        private IObservable<IRoutableViewModel> Navigate() => _navigationService.NavigateTo<WeatherForecastViewModel>();
    }
}