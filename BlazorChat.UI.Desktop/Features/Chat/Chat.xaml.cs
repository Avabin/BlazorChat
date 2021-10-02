using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using BlazorChat.Shared.Features.Chat.Models;
using BlazorChat.UI.Shared.Features.Chat;
using DynamicData.Binding;
using ReactiveUI;

namespace BlazorChat.UI.Desktop.Features.Chat
{
    public partial class Chat
    {
        public Chat(ChatViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Messages, v => v.MessagesListView.ItemsSource)
                    .DisposeWith(d);

                PostMessageButton
                    .Events().Click
                    .Select(_ =>
                        new Message(MessageContentTextBox.Text, UsernameTextBox.Text, DateTimeOffset.Now, "all"))
                    .InvokeCommand(ViewModel?.PostMessageCommand)
                    .DisposeWith(d);

                var isConnectedObservable = 
                    ViewModel?
                        .WhenValueChanged(vm => vm.IsConnected, false)
                        .Select(isConnected => isConnected ? "Connected" : "Connecting...");

                isConnectedObservable?
                    .Do(s => ConnectButton.Content = s)
                    .Subscribe()
                    .DisposeWith(d);

                ConnectButton.Events().Click
                    .Select(_ => Unit.Default)
                    .InvokeCommand(ViewModel?.ConnectCommand)
                    .DisposeWith(d);
                
                this.OneWayBind(ViewModel,
                        vm => vm.IsConnected,
                        v => v.ConnectButton.IsEnabled,
                        b => !b)
                    .DisposeWith(d);
            });
        }
    }
}