using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Veises.Common.Service
{
    public sealed class ServiceHost : IDisposable
    {
        private readonly IWebHost _webHost;

        internal ServiceHost(IWebHost webHost)
        {
            _webHost = webHost ?? throw new ArgumentNullException(nameof(webHost));
        }

        public static IServiceHostBuilder Create(Assembly[] assemblies, params string[] args) =>
            new ServiceHostBuilder(assemblies, args);

        public Task Start(CancellationToken cancellationToken) => _webHost.StartAsync(cancellationToken);

        public void Run() => _webHost.Run();

        public void Dispose() => _webHost.Dispose();
    }
}