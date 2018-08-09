using System;
using System.Reflection;
using Veises.Common.Service.IoC;
using Veises.Common.Service.Log;

namespace Veises.Common.Service
{
    public static class IServiceHostBuilderExtension
    {
        public static IServiceHostBuilder WithConfigFile(
            this IServiceHostBuilder serviceHostBuilder,
            string fileName)
        {
            if (serviceHostBuilder == null)
                throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.Configure(new ConfigFileHostConfigurator(fileName));
        }

        public static IServiceHostBuilder WithDefaultConfigFile(this IServiceHostBuilder serviceHostBuilder)
        {
            if (serviceHostBuilder == null)
                throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.WithConfigFile("appsettings.json");
        }

        public static IServiceHostBuilder WithLogging(this IServiceHostBuilder serviceHostBuilder)
        {
            return serviceHostBuilder.Configure(new LoggingHostConfigurator());
        }

        public static IServiceHostBuilder WithAssemblyDependencies(this IServiceHostBuilder serviceHostBuilder, Assembly assembly)
        {
            return serviceHostBuilder.Configure(new IocHostConfigurator(assembly));
        }
    }
}