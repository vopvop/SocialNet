using System;

namespace Veises.Common.Service.Versionning
{
    public static class IServiceHostBuilderExtension
    {
        public static IServiceHostBuilder WithApiVersionning(this IServiceHostBuilder serviceHostBuilder)
        {
            if (serviceHostBuilder == null)
                throw new ArgumentNullException(nameof(serviceHostBuilder));

            serviceHostBuilder.Configure(new VersioningHostConfigurator());

            return serviceHostBuilder;
        }
    }
}
