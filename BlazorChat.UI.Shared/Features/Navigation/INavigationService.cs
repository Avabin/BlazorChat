using System;
using ReactiveUI;

namespace BlazorChat.UI.Shared.Features.Navigation
{
    public interface INavigationService
    {
        RoutingState Router { get; }
        IObservable<IRoutableViewModel> NavigateTo<T>() where T : IRoutableViewModel;
        IObservable<IRoutableViewModel> NavigateToAndReset<T>() where T : IRoutableViewModel;
        IObservable<IRoutableViewModel?> NavigateBack();
    }
}