using System;

namespace BlazorChat.Shared.Features.Chat.Models
{
    public record Message(string Content, string Author, DateTimeOffset SentAt, string Target);
}