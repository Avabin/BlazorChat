using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorChat.Shared.Features.Chat.Models;
using BlazorChat.UI.Shared.Features.Navigation;
using DynamicData.Binding;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BlazorChat.UI.Shared.Features.Chat
{
    public class ChatViewModel : RoutableViewModel
    {
        private readonly IChatService _chatService;
        private readonly ILogger<ChatViewModel> _logger;

        public ReactiveCommand<Unit, Unit> ConnectCommand { get; }
        public ReactiveCommand<Message, Unit> PostMessageCommand { get; }
        public ObservableCollection<Message> Messages => _chatService.Messages;

        [Reactive] public bool IsConnected { get; set; }
        [Reactive] public string Username {get;set;}
        [Reactive] public string MessageContent {get;set;}

        public ChatViewModel(IScreen hostScreen, IChatService chatService, ILogger<ChatViewModel> logger) : base(hostScreen)
        {
            _chatService = chatService;
            _logger = logger;

            ConnectCommand = ReactiveCommand.CreateFromTask(_chatService.InitializeAsync);
            PostMessageCommand = ReactiveCommand.CreateFromTask<Message>(_chatService.PostMessageAsync);

            chatService.WhenValueChanged(x => x.IsConnected)
                .Do(b => IsConnected = b)
                .Subscribe();
            
            ConnectCommand.ThrownExceptions
                .Do(e => _logger.LogError(e, "Error while connecting to SignalR. {Exception}", e))
                .Subscribe();
            PostMessageCommand.ThrownExceptions
                .Do(e => _logger.LogError(e, "Error while sending message!. {Exception}", e))
                .Subscribe();
        }

        public override string? UrlPathSegment { get; } = "chat";
    }
}