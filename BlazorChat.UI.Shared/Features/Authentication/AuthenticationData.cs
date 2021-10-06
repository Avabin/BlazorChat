using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client;

namespace BlazorChat.UI.Shared.Features.Authentication
{
    public class AuthenticationData
    {
        public AuthenticationData(string uniqueId, string tokenType, string accessToken, List<string> scopes, DateTimeOffset? expiresOn, IAccount account)
        {
            UniqueId = uniqueId;
            TokenType = tokenType;
            AccessToken = accessToken;
            Scopes = scopes;
            ExpiresOn = expiresOn;
            Account = account;
        }

        public IAccount Account { get; }
        public string UniqueId { get; }
        public string TokenType { get; }
        public string AccessToken { get; }
        public List<string> Scopes { get; }
        public DateTimeOffset? ExpiresOn { get; }

        public void Deconstruct(out string uniqueId, out string tokenType, out string accessToken, out List<string> scopes, out DateTimeOffset? expiresOn, out IAccount account)
        {
            uniqueId = UniqueId;
            tokenType = TokenType;
            accessToken = AccessToken;
            scopes = Scopes;
            expiresOn = ExpiresOn;
            account = Account;
        }
        
        public static AuthenticationData FromMicrosoftAuthenticationResult(AuthenticationResult ar) 
            => new(ar.UniqueId, ar.TokenType, ar.AccessToken, ar.Scopes.ToList(), ar.ExpiresOn, ar.Account);
    }
}