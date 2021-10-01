using System;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorChat.Shared.Attributes
{
    /// <summary>
    /// Mark class to be registered in DI container
    /// Default lifetime is <c cref="ServiceLifetime.Transient"/>
    /// <see cref="ServiceLifetime"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; }

        public ServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            Lifetime = lifetime;
        }
    }
}