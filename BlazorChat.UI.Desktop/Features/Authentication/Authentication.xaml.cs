using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using BlazorChat.UI.Shared.Features.Authentication;
using BlazorChat.UI.Shared.Features.Navigation;
using BlazorChat.UI.Shared.Features.Profile;
using ReactiveUI;

namespace BlazorChat.UI.Desktop.Features.Authentication
{
    public partial class Authentication
    {
        public Authentication(AuthenticationViewModel viewModel, INavigationService navService)
        {
            InitializeComponent();

            ViewModel = viewModel;

            this.WhenActivated(d =>
            {
                MicrosoftLoginButton
                    .Events().Click
                    .Select(x => Unit.Default)
                    .InvokeCommand(ViewModel.AuthenticateCommand)
                    .DisposeWith(d);
                
                ViewModel.AuthenticateCommand
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Do(_ => navService.NavigateTo<ProfileViewModel>())
                    .Subscribe()
                    .DisposeWith(d);
            });
        }
    }
}