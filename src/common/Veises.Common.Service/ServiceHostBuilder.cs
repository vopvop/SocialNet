using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Veises.Common.Service.Utils;

namespace Veises.Common.Service
{
    internal sealed class ServiceHostBuilder : IServiceHostBuilder
    {
        private readonly IReadOnlyCollection<Assembly> _assemblies;

        private readonly ICollection<IHostConfigurator> _hostConfigurators;

        public ServiceHostBuilder(IReadOnlyCollection<Assembly> assemblies)
        {
            _assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));

            _hostConfigurators = new List<IHostConfigurator>();
        }

        public ServiceHost Build()
        {
            var webHost = WebHost
                .CreateDefaultBuilder()
                .UseKestrel()
                .ConfigureAppConfiguration(
                    (context, config) =>
                    {
                        var env = context.HostingEnvironment;

                        config
                            .SetBasePath(env.ContentRootPath)
                            .AddEnvironmentVariables();

                        foreach (var hostConfigurator in _hostConfigurators)
                        {
                            hostConfigurator.ConfigureApp()(context, config);
                        }
                    })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                    services.AddSingleton<IHttpContextProvider, HttpContextProvider>();

                    var mvcBuilder = services
                        .AddMvcCore(options => options.RespectBrowserAcceptHeader = true)
                        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                        .AddJsonFormatters()
                        .AddXmlSerializerFormatters();

                    foreach (var assembly in _assemblies)
                    {
                        mvcBuilder.AddApplicationPart(assembly);
                    }

                    foreach (var hostConfigurator in _hostConfigurators)
                    {
                        hostConfigurator.ConfigureServices()(services);
                    }
                })
                .Configure(builder =>
                {
                    builder.UseMvc();

                    var env = builder.ApplicationServices.GetRequiredService<IHostingEnvironment>();

                    if (env.IsDevelopment())
                    {
                        builder.UseDeveloperExceptionPage();
                    }
                    else
                    {
                        builder.UseHsts();
                    }

                    builder.UseHttpsRedirection();

                    foreach (var hostConfigurator in _hostConfigurators)
                    {
                        hostConfigurator.Configure()(builder);
                    }
                })
                .Build();

            return new ServiceHost(webHost);
        }

        public IServiceHostBuilder Configure(IHostConfigurator hostConfigurator)
        {
            if (hostConfigurator == null)
                throw new ArgumentNullException(nameof(hostConfigurator));

            _hostConfigurators.Add(hostConfigurator);

            return this;
        }
    }
}