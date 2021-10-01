using System;

namespace BlazorChat.UI.WebClient.Models
{
    public record Message(Guid Id, string Content, DateTimeOffset CreatedAt);
}