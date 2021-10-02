using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BlazorChat.Shared.Features.Chat.Models;
using ReactiveUI;

namespace BlazorChat.UI.Shared.Features.Chat
{
    public interface IChatService : IReactiveObject
    {
        ObservableCollection<Message> Messages { get; }
        bool IsConnected { get; }
        Task InitializeAsync();
        Task PostMessageAsync(Message message);
    }
}