using System;

namespace BlazorChat.Shared.Features.Chat.Models
{
    public class Message
    {
        public Message() : this("", "", DateTimeOffset.Now, "all") {}
        public Message(string content, string author, DateTimeOffset sentAt, string target)
        {
            Content = content;
            Author = author;
            SentAt = sentAt;
            Target = target;
        }

        public string Content { get; set; }
        public string Author { get; set; }
        public DateTimeOffset SentAt { get; set; }
        public string Target { get; set; }

        public void Deconstruct(out string content, out string author, out DateTimeOffset sentAt, out string target)
        {
            content = Content;
            author = Author;
            sentAt = SentAt;
            target = Target;
        }
    }
}