using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using BlazorChat.UI.Shared.Features.HostScreen;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace BlazorChat.UI.Desktop.Features.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(HostScreenViewModel mainWindowViewModel, ILogger<MainWindow> logger)
        {
            ViewModel = mainWindowViewModel;
            
            InitializeComponent();

            this.WhenActivated(d =>
            {
                // Bind the view model router to RoutedViewHost.Router property.
                this.OneWayBind(ViewModel, x => x.Router, x => x.RoutedViewHost.Router)
                    .DisposeWith(d);
                
                CounterButton.Events().Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel.NavigateCounterCommand)
                    .DisposeWith(d);

                ForecastButton.Events().Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel.NavigateForecastCommand)
                    .DisposeWith(d);
                
                ChatButton.Events().Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel.NavigateChatCommand)
                    .DisposeWith(d);
                
                AuthenticateButton.Events().Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel.NavigateLoginCommand)
                    .DisposeWith(d);
                
                Observable.Merge(
                        ViewModel.NavigateCounterCommand.ThrownExceptions,
                        ViewModel.NavigateChatCommand.ThrownExceptions,
                        ViewModel.NavigateForecastCommand.ThrownExceptions,
                        ViewModel.NavigateLoginCommand.ThrownExceptions)
                    .Do(ex => logger.LogError(ex, "An error occured during navigation!"))
                    .Subscribe()
                    .DisposeWith(d);
            });
        }
    }
}