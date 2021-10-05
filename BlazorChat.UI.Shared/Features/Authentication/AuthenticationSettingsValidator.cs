using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace BlazorChat.UI.Shared.Features.Authentication
{
    public class AuthenticationSettingsValidator : IValidateOptions<AuthenticationSettings>
    {
        public ValidateOptionsResult Validate(string name, AuthenticationSettings options)
        {
            var failures = new List<string>();
            if (string.IsNullOrWhiteSpace(options.TenantId))
                failures.Add("Tenant is empty!");
            if (string.IsNullOrWhiteSpace(options.ClientId))
                failures.Add("Client Id is empty!");
            if (string.IsNullOrWhiteSpace(options.RedirectUrl))
                failures.Add("Redirect Uri is empty!");

            return failures.Any() ? ValidateOptionsResult.Fail(failures) : ValidateOptionsResult.Success;
        }
    }
}