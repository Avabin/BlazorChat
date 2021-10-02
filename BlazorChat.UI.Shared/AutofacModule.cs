using System;
using System.Linq;
using System.Reflection;
using Autofac;
using BlazorChat.Shared.Attributes;
using BlazorChat.UI.Shared.Features.Navigation;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Module = Autofac.Module;

namespace BlazorChat.UI.Shared
{
    public class AutofacModule : Module
    {
        private readonly Assembly[] _assemblies;

        /// <summary>
        /// Create new module
        /// </summary>
        /// <param name="assemblies">Assemblies to be searched for types with <see cref="ServiceLifetime"/></param>
        public AutofacModule(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(_assemblies)
                .Where(t => t.GetInterfaces()
                    .Any(
                        i => i.IsGenericType
                             && i.GetGenericTypeDefinition() == typeof(IViewFor<>)))
                .AsSelf()
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(_assemblies)
                .Where(t => t.Name.Contains("ViewModel") && t.GetInterfaces().All(x => x != typeof(IScreen)))
                .AsSelf()
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(_assemblies)
                .Where(t => t.GetInterfaces().Any(x => x == typeof(IScreen)))
                .AsSelf()
                .As<IScreen>()
                .SingleInstance();

            // Search for all types with [Service] attribute
            var serviceTypes = _assemblies.SelectMany(x => x.GetTypes())
                .Select(x => (attr: x.GetCustomAttributes(true).OfType<ServiceAttribute>().SingleOrDefault(), type: x))
                .Where(x => x.attr is not null)
                .ToList();

            Console.WriteLine("Registering {0} services from autodiscovery", serviceTypes.Count);

            // Register those types with respect to defined lifetime
            foreach (var (attr, type) in serviceTypes)
                switch (attr!.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        builder.RegisterType(type).AsImplementedInterfaces().AsSelf().SingleInstance();
                        break;
                    case ServiceLifetime.Scoped:
                        builder.RegisterType(type).AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
                        break;
                    case ServiceLifetime.Transient:
                        builder.RegisterType(type).AsImplementedInterfaces().AsSelf();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            base.Load(builder);
        }
    }
}