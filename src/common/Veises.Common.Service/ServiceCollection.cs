using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Veises.Common.Service
{
    public sealed class ServiceCollection
    {
        internal ServiceCollection(
            [NotNull] IServiceCollection services,
            [NotNull] IConfiguration configuration)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [NotNull]
        public IServiceCollection Services { get; }

        [NotNull]
        public IConfiguration Configuration { get; }
    }
}