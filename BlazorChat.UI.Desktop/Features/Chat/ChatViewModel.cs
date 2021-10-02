using BlazorChat.UI.Shared.Features.Navigation;
using ReactiveUI;

namespace BlazorChat.UI.Desktop.Features.Chat
{
    public class ChatViewModel : RoutableViewModel
    {
        public ChatViewModel(IScreen hostScreen) : base(hostScreen)
        {
        }

        public override string? UrlPathSegment { get; } = "chat";
    }
}