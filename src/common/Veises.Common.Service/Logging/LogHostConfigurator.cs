using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Veises.Common.Logging;

namespace Veises.Common.Service.Logging
{
    internal sealed class LogHostConfigurator : IHostConfigurator
    {
        public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp()
        {
            return (builder, config) => { };
        }

        public Action<ServiceCollection> ConfigureServices()
        {
            return services => { LogProvider.Inject(services.Services); };
        }

        public Action<IApplicationBuilder> Configure()
        {
            return builder => { LogProvider.Inject(builder.ApplicationServices); };
        }
    }
}