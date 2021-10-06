using BlazorChat.UI.Shared.Features.Authentication;

namespace BlazorChat.UI.WebClient.Features.Authentication
{
    public partial class Authenticate
    {
        public Authenticate() : this(ServiceLocator.Get<AuthenticationViewModel>()) {}
        public Authenticate(AuthenticationViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}