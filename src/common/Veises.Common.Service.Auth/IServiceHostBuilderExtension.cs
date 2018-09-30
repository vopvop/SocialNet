using System;
using JetBrains.Annotations;
using Veises.Common.Service.Auth.Jwt;

namespace Veises.Common.Service.Auth
{
    public static class IServiceHostBuilderExtension
    {
        [NotNull]
        public static IServiceHostBuilder WithJwtAuth([NotNull] this IServiceHostBuilder serviceHostBuilder)
        {
            if (serviceHostBuilder == null)
                throw new ArgumentNullException(nameof(serviceHostBuilder));

            return serviceHostBuilder.Configure(new JwtAuthHostConfigurator());
        }
    }
}