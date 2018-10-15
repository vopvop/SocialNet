using System;
using System.Reflection;
using JetBrains.Annotations;
using Veises.Common.Service.IoC;
using Veises.Common.Service.Logging;
using Veises.Common.Service.Middleware;
using Veises.Common.Service.Security;
using Veises.Common.Service.Settings;
using Veises.Common.Service.Utils;

namespace Veises.Common.Service
{
    public static class IServiceHostBuilderExtension
    {
        [NotNull]
        public static IServiceHostBuilder WithConfigFile(
            [NotNull] this IServiceHostBuilder serviceHostBuilder, 
            [NotNull] string fileName)
        {
            if (serviceHostBuilder == null) throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.Configure(new ConfigFileHostConfigurator(fileName));
        }

        [NotNull]
        public static IServiceHostBuilder WithDefaultConfigFile([NotNull] this IServiceHostBuilder serviceHostBuilder)
        {
            if (serviceHostBuilder == null) throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.WithConfigFile("appsettings.json");
        }

        [NotNull]
        public static IServiceHostBuilder WithLogging([NotNull] this IServiceHostBuilder serviceHostBuilder)
        {
            if (serviceHostBuilder == null) throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.Configure(new LogHostConfigurator());
        }
        
        [NotNull]
        public static IServiceHostBuilder WithAssemblyDependencies([NotNull] this IServiceHostBuilder serviceHostBuilder,
            Assembly assembly)
        {
            if (serviceHostBuilder == null) throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.Configure(new IocHostConfigurator(assembly));
        }

        [NotNull]
        public static IServiceHostBuilder WithRequestMiddleware<TRequestExecutor>(
            [NotNull] this IServiceHostBuilder serviceHostBuilder)
            where TRequestExecutor : class, IRequestMiddleware
        {
            if (serviceHostBuilder == null) throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.Configure(new RequestMiddlewareConfigurator<TRequestExecutor>());
        }

        [NotNull]
        public static IServiceHostBuilder WithHttps([NotNull] this IServiceHostBuilder builder, bool useForDev = false)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            return builder.Configure(new HttpsHostConfigurator(useForDev));
        }

        [NotNull]
        public static IServiceHostBuilder WithSessionId([NotNull] this IServiceHostBuilder serviceHostBuilder)
        {
            if (serviceHostBuilder == null) throw new ArgumentNullException(nameof(serviceHostBuilder));
            
            return serviceHostBuilder
                .Configure(new SessionIdHostConfigurator())
                .WithRequestMiddleware<SessionIdRequestMiddleware>();
        }
    }
}