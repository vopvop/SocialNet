using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Veises.Common.Service.Settings
{
    internal sealed class ConfigFileHostConfigurator : IHostConfigurator
    {
        private const string ConfigFileExt = "json";

        private readonly string _fileName;

        public ConfigFileHostConfigurator([NotNull] string fileName)
        {
            _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        }

        public Action<IApplicationBuilder> Configure()
        {
            return builder => { };
        }

        public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp()
        {
            return (context, config) =>
            {
                config
                    .AddJsonFile($"{_fileName}.{ConfigFileExt}", true, true)
                    .AddJsonFile($"{_fileName}.{context.HostingEnvironment.EnvironmentName}.{ConfigFileExt}", true,
                        true);
            };
        }

        public Action<ServiceCollection> ConfigureServices()
        {
            return collection => { collection.Services.AddSingleton(typeof(ISetting<>), typeof(Setting<>)); };
        }
    }
}