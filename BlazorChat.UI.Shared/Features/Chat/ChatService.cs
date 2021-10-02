using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorChat.Shared;
using BlazorChat.Shared.Attributes;
using BlazorChat.Shared.Features.Chat.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RestEase;

namespace BlazorChat.UI.Shared.Features.Chat
{
    [Service(ServiceLifetime.Singleton)]
    public class ChatService : ReactiveObject, IChatService
    {
        private HubConnection _hubConnection;
        private readonly string _functionBaseUri;
        private readonly string _chatHubUrl;
        private IFunctionsApi _client;
        public ObservableCollection<Message> Messages { get; }
        [Reactive] public bool IsConnected { get; private set;} = false;

        public ChatService(IConfiguration configuration)
        {
            _functionBaseUri = configuration["FunctionsBaseUrl"];
            _chatHubUrl = configuration["ChatHubUrl"];
            _client = RestClient.For<IFunctionsApi>(_functionBaseUri);
            
            Messages = new ObservableCollection<Message>();
        }

        public async Task InitializeAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_chatHubUrl)
                .Build();

            _hubConnection.On<Message>("ClientMessage", message =>
            {
                Messages.Add(message);
            });
            
            _hubConnection.Reconnected += _ =>
            {
                IsConnected = true;
                return Task.CompletedTask;
            };

            _hubConnection.Reconnecting += exception =>
            {
                IsConnected = false;
                return Task.CompletedTask;
            };

            await _hubConnection.StartAsync();
            IsConnected = true;
        }

        public Task PostMessageAsync(Message message) => _client.PostMessageAsync(message);
    }
}