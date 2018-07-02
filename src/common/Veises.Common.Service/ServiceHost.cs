using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Veises.Common.Service.Utils;

namespace Veises.Common.Service
{
    public sealed class ServiceHost
    {
        private readonly IWebHost _webHost;

        private ServiceHost(IWebHost webHost)
        {
            _webHost = webHost ?? throw new ArgumentNullException(nameof(webHost));
        }

        public static ServiceHost Create(params Assembly[] assemblies)
        {
            return new ServiceHost(CreateWebHost(assemblies));
        }

        private static IWebHost CreateWebHost(IReadOnlyCollection<Assembly> assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));

            return WebHost
                .CreateDefaultBuilder()
                .UseKestrel()
                .ConfigureAppConfiguration(
                    (context, config) =>
                    {
                        var env = context.HostingEnvironment;

                        config
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", true, true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                            .AddEnvironmentVariables();
                    })
                .ConfigureServices(services => services.AddSingleton(new AssemblyProvider(assemblies)))
                .UseStartup<Startup>()
                .Build();
        }

        public static void Run(params Assembly[] assemblies)
        {
            using (var webHost = CreateWebHost(assemblies))
            {
                webHost.Run();
            }
        }

        public Task Start(CancellationToken cancellationToken)
        {
            return _webHost.StartAsync(cancellationToken);
        }
    }
}