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
            var allTypes = _assemblies.SelectMany(x => x.GetTypes()).ToArray();
            var screen = allTypes.Single(t => t.IsAssignableTo(typeof(IScreen)));
            var views = allTypes
                .Where(t => t.IsAssignableTo(typeof(IViewFor<>)) && !t.IsAssignableTo(typeof(IScreen)))
                .ToList();
            var viewModels = allTypes
                .Where(t => t.IsAssignableTo(typeof(ReactiveObject)) || t.IsAssignableTo(typeof(RoutableViewModel)))
                .ToList();
            var serviceTypes = allTypes
                .Select(x => (attr: x.GetCustomAttributes(true).OfType<ServiceAttribute>().SingleOrDefault(), type: x))
                .Where(x => x.attr is not null)
                .ToList();
            Console.WriteLine("Screen {0}", screen?.Name);
            Console.WriteLine("Views: Count = {0}, Types = {1}", views.Count, string.Join(", ", views));
            Console.WriteLine("ViewModels: Count = {0}, Types = {1}", viewModels.Count, string.Join(", ", viewModels));
            Console.WriteLine("Services: Count = {0}, Types = {1}", serviceTypes.Count, string.Join(", ", serviceTypes));

            builder.RegisterType(screen).AsSelf().AsImplementedInterfaces().SingleInstance();

            foreach (var type in views.Concat(viewModels))
            {
                builder.RegisterType(type).AsSelf().AsImplementedInterfaces().SingleInstance();
            }


            foreach (var (attr, type) in serviceTypes)
            {
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
            }

            base.Load(builder);
        }
    }
}