using Blazorade.Msal.Security;
using Microsoft.Identity.Client;

namespace BlazorChat.UI.WebClient.Features.Authentication
{
    public class AuthAccount : IAccount
    {
        public AuthAccount(string username, string environment)
        {
            Username = username;
            Environment = environment;
        }

        public AuthAccount(Account account)
        {
            Username = account.Name;
            Environment = account.Environment;
        }

        public string Username { get; }
        public string Environment { get; }
        
        public AccountId? HomeAccountId { get; } = null;
    }
}