using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Veises.Common.Service
{
    internal sealed class ConfigFileHostConfigurator: IHostConfigurator
    {
        private const string ConfigFileExt = "json";

        private readonly string _fileName;

        public ConfigFileHostConfigurator(string fileName)
        {
            _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        }

		public Action<IApplicationBuilder> Configure() => builder => { };

		public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp()
		{
			return (context, config) =>
			{
				config
					.AddJsonFile($"{_fileName}.{ConfigFileExt}", true, true)
					.AddJsonFile($"{_fileName}.{context.HostingEnvironment.EnvironmentName}.{ConfigFileExt}", true, true);
			};
		}

		public Action<IServiceCollection> ConfigureServices() => services => { };
	}
}