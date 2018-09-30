using Microsoft.AspNetCore.Hosting;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Veises.Common.Service
{
    public sealed class ServiceHost : IDisposable
    {
        [NotNull]
        private readonly IWebHost _webHost;

        internal ServiceHost([NotNull] IWebHost webHost)
        {
            _webHost = webHost ?? throw new ArgumentNullException(nameof(webHost));
        }

        public static IServiceHostBuilder Create(
            [NotNull] Assembly[] assemblies,
            [CanBeNull] params string[] args) =>
            new ServiceHostBuilder(assemblies, args);

        public Task Start(CancellationToken cancellationToken) => _webHost.StartAsync(cancellationToken);

        public void Run() => _webHost.Run();

        public void Dispose() => _webHost.Dispose();
    }
}