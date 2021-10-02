using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BlazorChat.Shared.Features.Chat.Models;
using BlazorChat.UI.Shared.Features.Chat;
using Blazorise;
using Blazorise.DataGrid;
using DynamicData.Binding;
using Microsoft.AspNetCore.Components;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BlazorChat.UI.WebClient.Features.Chat
{
    public partial class Chat
    {
        public Chat() : this(ServiceLocator.Get<ChatViewModel>())
        {
            
        }
        public Chat(ChatViewModel viewModel)
        {
            ViewModel = viewModel;

            this.WhenActivated(d =>
            {
                ViewModel.Messages
                    .ObserveCollectionChanges()
                    .Do(_ => StateHasChanged())
                    .Subscribe()
                    .DisposeWith(d);
            });
        }
        
        private void Callback()
        {
            var userName = ViewModel.Username;
            var content = ViewModel.MessageContent;
            var message = new Message(userName, content, DateTimeOffset.Now, "all");

            ViewModel.PostMessageCommand.Execute(message).Subscribe();
        }
    }
}