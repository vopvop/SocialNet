using System;
using Veises.Common.Service;

namespace Veises.Common.Services.Swagger
{
	public static class IServiceHostBuilderExtension
    {
		public static IServiceHostBuilder WithSwagger(this IServiceHostBuilder hostBuilder, string title, string description)
		{
			if (hostBuilder == null)
				throw new ArgumentNullException(nameof(hostBuilder));

			hostBuilder.Configure(new SwaggerHostConfigurator(title, description));

			return hostBuilder;
		}
    }
}
