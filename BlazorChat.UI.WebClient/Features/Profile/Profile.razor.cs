using BlazorChat.UI.Shared.Features.Profile;
using Blazorise;
using ReactiveUI;

namespace BlazorChat.UI.WebClient.Features.Profile
{
    public partial class Profile
    {
        public Profile() : this(ServiceLocator.Get<ProfileViewModel>()) {}
        public Profile(ProfileViewModel viewModel)
        {
            ViewModel = viewModel;

            this.WhenActivated(d =>
            {
            });
        }
    }
}