using Microsoft.Extensions.DependencyInjection;

namespace BlazorChat.UI.WebClient
{
    public static class ServiceLocator
    {
        public static T Get<T>() where T : notnull => Program.ServiceProvider.GetRequiredService<T>();
    }
}