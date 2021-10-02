using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BlazorChat.UI.Shared.Features.Navigation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace BlazorChat.UI.WebClient.Features.Navigation
{
    public class BlazorNavigationService : INavigationService, IDisposable
    {
        private readonly IList<IRoutableViewModel> _viewModels;
        private readonly NavigationManager _navigationManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BlazorNavigationService> _logger;
        private readonly CompositeDisposable _disposables;
        public RoutingState Router { get; }

        public BlazorNavigationService(IList<IRoutableViewModel> viewModels, NavigationManager navigationManager, IServiceProvider serviceProvider, ILogger<BlazorNavigationService> logger)
        {
            _viewModels = viewModels;
            _navigationManager = navigationManager;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _disposables = new CompositeDisposable(); 
            
            Router = new RoutingState();

            Router.CurrentViewModel.Subscribe(OnNext).DisposeWith(_disposables);
        }

        private void OnNext(IRoutableViewModel? obj)
        {
            // Validate parameter
            if (obj is not { } vm || string.IsNullOrWhiteSpace(vm.UrlPathSegment)) return;
            _logger.LogTrace("OnNext: {ViewModelName}|{UrlSegment}", vm.GetType().Name, vm.UrlPathSegment);
            // Navigate
            _navigationManager.NavigateTo(vm.UrlPathSegment);
        }

        public IObservable<IRoutableViewModel> NavigateTo<T>() where T : IRoutableViewModel =>
            Navigate<T>(Router.Navigate);

        public IObservable<IRoutableViewModel> NavigateToAndReset<T>() where T : IRoutableViewModel =>
            Navigate<T>(Router.NavigateAndReset);

        public IObservable<IRoutableViewModel?> NavigateBack() => 
            Router.NavigationStack.Any() 
                ? Router.NavigateBack.Execute() 
                : Observable.Empty<IRoutableViewModel?>();

        public void Dispose() => _disposables?.Dispose();

        private IObservable<IRoutableViewModel> Navigate<T>(ReactiveCommandBase<IRoutableViewModel, IRoutableViewModel> navigateCommand)
            where T : IRoutableViewModel
        {
            var vm = _viewModels.OfType<T>().Single();

            return navigateCommand.Execute(vm);
        }
    }
}