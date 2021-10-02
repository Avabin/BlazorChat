using System;

namespace BlazorChat.Shared.Features.Chat.Models
{
    public record ChatUser(string Username, DateTimeOffset CreatedAt, DateTimeOffset LastOnline);
}