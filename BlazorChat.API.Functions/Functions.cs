using System.Threading.Tasks;
using BlazorChat.Shared.Features.Chat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace BlazorChat.API.Functions
{
    public class Functions
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate( 
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "serverlessSample")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
        
        [FunctionName("messages")]
        public static Task SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] Message clientMessage,
            [SignalR(HubName = "chatHub")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "clientMessage",
                    Arguments = new object[] { clientMessage }
                });
        }
    }
}