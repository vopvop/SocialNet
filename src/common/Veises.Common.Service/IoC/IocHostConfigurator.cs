using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Veises.Common.Extensions;

namespace Veises.Common.Service.IoC
{
    internal sealed class IocHostConfigurator : IHostConfigurator
    {
        private readonly Assembly _sourceAssembly;

        public IocHostConfigurator(Assembly sourceAssembly)
        {
            _sourceAssembly = sourceAssembly ?? throw new ArgumentNullException(nameof(sourceAssembly));
        }

        public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp() => (context, builder) => { };

        public Action<IServiceCollection> ConfigureServices()
        {
            return services =>
            {
                var injections = GetDependencyInjections();

                foreach (var injection in injections)
                {
                    switch (injection.Item3)
                    {
                        case DependencyScope.Scoped:
                            services.AddScoped(injection.Item2, injection.Item1);
                            break;
                        case DependencyScope.Singleton:
                            services.AddSingleton(injection.Item2, injection.Item1);
                            break;
                        case DependencyScope.Transient:
                            services.AddTransient(injection.Item2, injection.Item1);
                            break;
                        default:
                            throw new ArgumentException($"Unknown injection scope type {injection.Item3.Escaped()}.");
                    }
                }
            };
        }

        private IEnumerable<Tuple<Type, Type, DependencyScope>> GetDependencyInjections()
        {
            foreach (var assemblyType in _sourceAssembly.GetTypes())
            {
                var injectedDependencyAttribute = assemblyType.GetCustomAttribute<InjectDependencyAttribute>();

                if (injectedDependencyAttribute == null)
                    continue;

                yield return new Tuple<Type, Type, DependencyScope>(assemblyType, assemblyType,
                    injectedDependencyAttribute.Scope);

                var implementedInterfaces = assemblyType.GetInterfaces();

                if (implementedInterfaces.Length == 0)
                    continue;

                foreach (var implementedInterface in implementedInterfaces)
                {
                    if (implementedInterface.IsGenericType)
                    {
                        var genericInterfactType = implementedInterface.GetGenericTypeDefinition();

                        yield return new Tuple<Type, Type, DependencyScope>(
                            assemblyType,
                            genericInterfactType,
                            injectedDependencyAttribute.Scope);
                    }
                    else
                    {
                        yield return new Tuple<Type, Type, DependencyScope>(
                            assemblyType,
                            implementedInterface,
                            injectedDependencyAttribute.Scope);
                    }
                }
            }
        }

        public Action<IApplicationBuilder> Configure() => builder => { };
    }
}