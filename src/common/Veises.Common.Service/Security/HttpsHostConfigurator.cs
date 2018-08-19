using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Veises.Common.Service.Security
{
    internal sealed class HttpsHostConfigurator : IHostConfigurator
    {
        private readonly bool _useForDev;

        public HttpsHostConfigurator(bool useForDev)
        {
            _useForDev = useForDev;
        }

        public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp() => (context, builder) => { };

        public Action<IServiceCollection> ConfigureServices() => (services) => { };

        public Action<IApplicationBuilder> Configure() => (builder) =>
        {
            var env = builder.ApplicationServices.GetRequiredService<IHostingEnvironment>();

            var isInDev = env.IsDevelopment();

            if (isInDev && _useForDev || isInDev == false)
            {
                builder.UseHsts();
                
                builder.UseHttpsRedirection();
            }
        };
    }
}