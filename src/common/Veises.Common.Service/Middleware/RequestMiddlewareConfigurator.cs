using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Veises.Common.Service.Middleware
{
    internal sealed class RequestMiddlewareConfigurator<TRequestMiddleware> : IHostConfigurator
        where TRequestMiddleware : class, IRequestMiddleware
    {
        public Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureApp()
        {
            return (context, builder) => { };
        }

        public Action<IServiceCollection> ConfigureServices()
        {
            return services =>
            {
                services.AddTransient(typeof(TRequestMiddleware));
                services.AddTransient(typeof(RequestMiddlewareWrapper<TRequestMiddleware>));
            };
        }

        public Action<IApplicationBuilder> Configure()
        {
            return builder =>
            {
                builder.UseMiddleware(typeof(RequestMiddlewareWrapper<TRequestMiddleware>));
            };
        }
    }
}