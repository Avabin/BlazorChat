using System;

namespace BlazorChat.UI.WebClient.Models
{
    public record Chat(Guid Id, string Name, string LastMessage, DateTimeOffset CreatedAt);
}