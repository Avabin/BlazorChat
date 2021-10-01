using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;

namespace BlazorChat.UI.Desktop.Features.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            ViewModel = mainWindowViewModel;
            
            InitializeComponent();

            this.WhenActivated(d =>
            {
                // Bind the view model router to RoutedViewHost.Router property.
                this.OneWayBind(ViewModel, x => x.Router, x => x.RoutedViewHost.Router)
                    .DisposeWith(d);
                
                
            });
        }

        private void NavigateToCounter(object sender, RoutedEventArgs e)
        {
            ViewModel?.NavigateCommand?.Execute()?.Subscribe();
        }
    }
}