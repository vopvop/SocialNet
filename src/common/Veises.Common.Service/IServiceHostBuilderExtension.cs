using System;
using System.Reflection;
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
        public static IServiceHostBuilder WithConfigFile(this IServiceHostBuilder serviceHostBuilder, string fileName)
        {
            if (serviceHostBuilder == null) throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.Configure(new ConfigFileHostConfigurator(fileName));
        }

        public static IServiceHostBuilder WithDefaultConfigFile(this IServiceHostBuilder serviceHostBuilder)
        {
            if (serviceHostBuilder == null) throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.WithConfigFile("appsettings.json");
        }

        public static IServiceHostBuilder WithLogging(this IServiceHostBuilder serviceHostBuilder)
        {
            if (serviceHostBuilder == null) throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.Configure(new LogHostConfigurator());
        }
        
        public static IServiceHostBuilder WithAssemblyDependencies(this IServiceHostBuilder serviceHostBuilder,
            Assembly assembly)
        {
            if (serviceHostBuilder == null) throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.Configure(new IocHostConfigurator(assembly));
        }

        public static IServiceHostBuilder WithRequestMiddleware<TRequestExecutor>(
            this IServiceHostBuilder serviceHostBuilder)
            where TRequestExecutor : class, IRequestMiddleware
        {
            if (serviceHostBuilder == null) throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.Configure(new RequestMiddlewareConfigurator<TRequestExecutor>());
        }

        public static IServiceHostBuilder WithHttps(this IServiceHostBuilder builder, bool useForDev = false)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            return builder.Configure(new HttpsHostConfigurator(useForDev));
        }

        public static IServiceHostBuilder WithSessionId(this IServiceHostBuilder serviceHostBuilder)
        {
            return serviceHostBuilder
                .Configure(new SessionIdHostConfigurator())
                .WithRequestMiddleware<SessionIdRequestMiddleware>();
        }
    }
}