#nullable enable
using System;
using BlazorChat.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace BlazorChat.UI.Shared.Services
{
    [Service]
    public class AutofacViewLocator : IViewLocator
    {
        private readonly Lazy<IServiceProvider> _lazyServiceProvider;
        private IServiceProvider ServiceProvider => _lazyServiceProvider.Value;

        public AutofacViewLocator(Lazy<IServiceProvider> lazyServiceProvider)
        {
            _lazyServiceProvider = lazyServiceProvider;
        }
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
            var iViewForType = typeof(IViewFor<>).MakeGenericType(viewModel!.GetType());

            return (IViewFor?) ServiceProvider.GetService(iViewForType);
        }
    }
}