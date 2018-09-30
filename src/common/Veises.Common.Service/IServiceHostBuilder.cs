using JetBrains.Annotations;

namespace Veises.Common.Service
{
    public interface IServiceHostBuilder
    {
        [NotNull]
        IServiceHostBuilder Configure([NotNull] IHostConfigurator hostConfigurator);

        [NotNull]
        ServiceHost Build();
    }
}