using System;

namespace Veises.Common.Service.Auth
{
    public static class IServiceHostBuilderExtension
    {
        public static IServiceHostBuilder WithJwtAuth(this IServiceHostBuilder serviceHostBuilder)
        {
            if (serviceHostBuilder == null)
                throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.Configure(new JwtAuthHostConfigurator());
        }
    }
}