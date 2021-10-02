using System.Collections.Generic;

namespace BlazorChat.Shared.Features.Chat.Models
{
    public record Chatroom(List<ChatUser> Members, List<Message> Messages);
}