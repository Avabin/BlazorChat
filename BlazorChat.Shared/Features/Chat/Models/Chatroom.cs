using System.Collections.Generic;

namespace BlazorChat.Shared.Features.Chat.Models
{
    public class Chatroom
    {
        public Chatroom(List<ChatUser> members, List<Message> messages)
        {
            Members = members;
            Messages = messages;
        }

        public List<ChatUser> Members { get; init; }
        public List<Message> Messages { get; init; }

        public void Deconstruct(out List<ChatUser> members, out List<Message> messages)
        {
            members = Members;
            messages = Messages;
        }
    }
}