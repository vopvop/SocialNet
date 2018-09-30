using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Veises.Common.Service.Utils;

namespace Veises.Common.Service
{
    internal sealed class ServiceHostBuilder : IServiceHostBuilder
    {
        private readonly IReadOnlyCollection<Assembly> _assemblies;

        private readonly string[] _args;

        private readonly ICollection<IHostConfigurator> _hostConfigurators;

        public ServiceHostBuilder([NotNull] IReadOnlyCollection<Assembly> assemblies, params string[] args)
        {
            _assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
            _args = args;
            
            if (_assemblies.Count == 0)
                throw new ArgumentException("No one assembly was specified.");

            _hostConfigurators = new List<IHostConfigurator>();
        }

        public ServiceHost Build()
        {
            IConfiguration configuration = null;
            
            var webHost = WebHost
                .CreateDefaultBuilder()
                .UseKestrel()
                .ConfigureAppConfiguration(
                    (context, config) =>
                    {
                        var env = context.HostingEnvironment;

                        config
                            .AddCommandLine(_args)
                            .SetBasePath(env.ContentRootPath)
                            .AddEnvironmentVariables();

                        foreach (var hostConfigurator in _hostConfigurators)
                        {
                            hostConfigurator.ConfigureApp()(context, config);
                        }

                        configuration = config.Build();
                    })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                    services.AddSingleton<IHttpContextProvider, HttpContextProvider>();

                    services.AddSingleton<ISystemEnvironment>(provider => new SystemEnvironment(_args));
                    services.AddSingleton<ITimeService, TimeService>();

                    var mvcBuilder = services
                        .AddMvcCore(options => options.RespectBrowserAcceptHeader = true)
                        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                        .AddJsonFormatters()
                        .AddXmlSerializerFormatters();
                    
                    var collection = new ServiceCollection(services, configuration);

                    foreach (var hostConfigurator in _hostConfigurators)
                    {
                        hostConfigurator.ConfigureServices()(collection);
                    }

                    foreach (var assembly in _assemblies)
                    {
                        mvcBuilder.AddApplicationPart(assembly);
                    }
                })
                .Configure(builder =>
                {
                    var env = builder.ApplicationServices.GetRequiredService<IHostingEnvironment>();

                    if (env.IsDevelopment())
                    {
                        builder.UseDeveloperExceptionPage();
                    }

                    foreach (var hostConfigurator in _hostConfigurators)
                    {
                        hostConfigurator.Configure()(builder);
                    }
                    
                    builder.UseMvc();
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