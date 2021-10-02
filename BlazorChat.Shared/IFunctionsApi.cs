using System.Threading.Tasks;
using BlazorChat.Shared.Features.Chat.Models;
using RestEase;

namespace BlazorChat.Shared
{
    public interface IFunctionsApi
    {
        [Post("messages")]
        Task PostMessageAsync([Body] Message message);
    }
}