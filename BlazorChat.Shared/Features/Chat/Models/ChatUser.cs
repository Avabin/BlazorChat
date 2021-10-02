using System;

namespace BlazorChat.Shared.Features.Chat.Models
{
    public class ChatUser
    {
        public ChatUser(string username, DateTimeOffset createdAt, DateTimeOffset lastOnline)
        {
            Username = username;
            CreatedAt = createdAt;
            LastOnline = lastOnline;
        }

        public string Username { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset LastOnline { get; init; }

        public void Deconstruct(out string username, out DateTimeOffset createdAt, out DateTimeOffset lastOnline)
        {
            username = Username;
            createdAt = CreatedAt;
            lastOnline = LastOnline;
        }
    }
}