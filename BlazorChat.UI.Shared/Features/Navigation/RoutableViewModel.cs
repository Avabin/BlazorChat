using ReactiveUI;

namespace BlazorChat.UI.Shared.Features.Navigation
{
    public abstract class RoutableViewModel : ReactiveObject, IRoutableViewModel
    {

        protected RoutableViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            Router = hostScreen.Router;
        }
        
        public abstract string? UrlPathSegment { get; }
        public IScreen HostScreen { get; }
        public RoutingState Router { get; }
    }
}