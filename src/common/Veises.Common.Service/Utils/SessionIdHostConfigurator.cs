using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Veises.Common.Service.Utils
{
    internal sealed class SessionIdHostConfigurator : IHostConfigurator
    {
        public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp() => (context, builder) => { };

        public Action<IServiceCollection> ConfigureServices() => (services) =>
        {
            services.AddTransient<SessionIdProvider>();
            services.AddTransient<ISessionInfoProvider, SessionInfoProvider>();
        };

        public Action<IApplicationBuilder> Configure() => (builder) => { };
    }
}