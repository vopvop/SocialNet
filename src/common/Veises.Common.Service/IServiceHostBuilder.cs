namespace Veises.Common.Service
{
    public interface IServiceHostBuilder
    {
        IServiceHostBuilder Configure(IHostConfigurator hostConfigurator);

        ServiceHost Build();
    }
}