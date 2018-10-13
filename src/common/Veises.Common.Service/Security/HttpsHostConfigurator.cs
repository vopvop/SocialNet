using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Veises.Common.Service.Log;

namespace Veises.Common.Service.Security
{
    internal sealed class HttpsHostConfigurator : IHostConfigurator
    {
        private static readonly ILog Log = LogProvider.GetLogFor<HttpsHostConfigurator>();
        
        private readonly bool _useForDev;

        public HttpsHostConfigurator(bool useForDev)
        {
            _useForDev = useForDev;
        }

        public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp() => (context, builder) => { };

        public Action<ServiceCollection> ConfigureServices() => (collection) => { };

        public Action<IApplicationBuilder> Configure() => (builder) =>
        {
            var env = builder.ApplicationServices.GetRequiredService<IHostingEnvironment>();

            var isInDev = env.IsDevelopment();

            if (isInDev && _useForDev || isInDev == false)
            {
                builder.UseHsts();
                
                builder.UseHttpsRedirection();
                
                Log.WriteDebug("HTTPS enabled.");
            }
        };
    }
}