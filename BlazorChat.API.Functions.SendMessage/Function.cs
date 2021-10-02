using System.Threading.Tasks;
using BlazorChat.Shared.Features.Chat.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;

namespace BlazorChat.API.Functions.SendMessage
{
    public static class Function
    {
        [FunctionName("messages")]
        public static Task SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] Message clientMessage,
            [SignalR(HubName = "chatHub")] IAsyncCollector<SignalRMessage> signalRMessages,
            ILogger logger)
        {
            logger.LogInformation("New message from {Author} to {Target} sent at {SentAt} with content \"{Content}\"",
                clientMessage.Author, clientMessage.Target, clientMessage.SentAt, clientMessage.Content);
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "ClientMessage",
                    Arguments = new object[] { clientMessage }
                });
        }
    }
}