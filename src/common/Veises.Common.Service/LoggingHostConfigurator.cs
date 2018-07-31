using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.Extensions.Configuration;

namespace Veises.Common.Service
{
	internal sealed class LoggingHostConfigurator: IHostConfigurator
	{
		public Action<IApplicationBuilder> Configure()
		{
			return builder =>
			{
				var factory = builder.ApplicationServices.GetRequiredService<ILoggerFactory>();

				if (builder.ApplicationServices.GetRequiredService<IHostingEnvironment>().IsDevelopment())
				{
					factory.AddConsole();
					factory.AddDebug(LogLevel.Debug);
				}
			};
		}

		public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp() => (context, config) => { };

		public Action<IServiceCollection> ConfigureServices()
		{
			return services => { };
		}
	}
}