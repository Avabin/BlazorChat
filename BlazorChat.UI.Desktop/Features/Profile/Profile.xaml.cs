using System.Reactive.Disposables;
using System.Windows.Controls;
using BlazorChat.UI.Shared.Features.Profile;
using ReactiveUI;

namespace BlazorChat.UI.Desktop.Features.Profile
{
    public partial class Profile
    {
        public Profile(ProfileViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel,
                        vm => vm.Username,
                        v => v.UsernameTextBox.Text)
                    .DisposeWith(d);
            });
        }
    }
}