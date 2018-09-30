using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Veises.Common.Extensions;

namespace Veises.Common.Service.IoC
{
    internal sealed class IocHostConfigurator : IHostConfigurator
    {
        [NotNull]
        private readonly Assembly _sourceAssembly;

        public IocHostConfigurator([NotNull] Assembly sourceAssembly)
        {
            _sourceAssembly = sourceAssembly ?? throw new ArgumentNullException(nameof(sourceAssembly));
        }

        public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp()
        {
            return (context, builder) => { };
        }

        public Action<ServiceCollection> ConfigureServices()
        {
            return collection =>
            {
                var injections = GetDependencyInjections();

                foreach (var injection in injections)
                    switch (injection.Scope)
                    {
                        case DependencyScope.Scoped:
                            collection.Services.AddScoped(injection.Service, injection.Implementation);
                            break;
                        case DependencyScope.Singleton:
                            collection.Services.AddSingleton(injection.Service, injection.Implementation);
                            break;
                        case DependencyScope.Transient:
                            collection.Services.AddTransient(injection.Service, injection.Implementation);
                            break;
                        default:
                            throw new ArgumentException($"Unknown injection scope type {injection.Scope.Escaped()}.");
                    }
            };
        }

        public Action<IApplicationBuilder> Configure()
        {
            return builder => { };
        }

        private IEnumerable<(Type Service, Type Implementation, DependencyScope Scope)> GetDependencyInjections()
        {
            foreach (var assemblyType in _sourceAssembly.GetTypes())
            {
                var injectedDependencyAttribute = assemblyType.GetCustomAttribute<InjectDependencyAttribute>();

                if (injectedDependencyAttribute == null)
                    continue;

                yield return (assemblyType, assemblyType, injectedDependencyAttribute.Scope);

                var implementedInterfaces = assemblyType.GetInterfaces();

                if (implementedInterfaces.Length == 0)
                    continue;

                foreach (var implementedInterface in implementedInterfaces)
                    if (implementedInterface.IsGenericType)
                    {
                        var genericInterfactType = implementedInterface.GetGenericTypeDefinition();

                        yield return (
                            genericInterfactType,
                            assemblyType,
                            injectedDependencyAttribute.Scope);
                    }
                    else
                    {
                        yield return (
                            implementedInterface,
                            assemblyType,
                            injectedDependencyAttribute.Scope);
                    }
            }
        }
    }
}