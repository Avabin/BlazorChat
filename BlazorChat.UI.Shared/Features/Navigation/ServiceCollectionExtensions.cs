using Microsoft.Extensions.DependencyInjection;

namespace BlazorChat.UI.Shared.Features.Navigation
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add custom implementation of <see cref="INavigationService"/>
        /// </summary>
        /// <param name="collection">Service collection</param>
        /// <typeparam name="TNavigation">Type of implementation of <see cref="INavigationService"/></typeparam>
        /// <returns>collection</returns>
        public static IServiceCollection AddNavigation<TNavigation>(this IServiceCollection collection)
            where TNavigation : class, INavigationService 
            => collection.AddSingleton<INavigationService, TNavigation>();

        /// <summary>
        /// Add <see cref="NavigationService"/>
        /// </summary>
        /// <param name="collection">Service collection</param>
        /// <returns>collection</returns>
        public static IServiceCollection AddNavigation(this IServiceCollection collection)
            => collection.AddSingleton<INavigationService, NavigationService>();
    }
}