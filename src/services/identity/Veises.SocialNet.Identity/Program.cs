using Veises.Common.Service;
using Veises.Common.Service.Auth;
using Veises.Common.Service.Versionning;
using Veises.Common.Services.Swagger;

namespace Veises.SocialNet.Identity
{
    public class Program
    {
        public static void Main()
        {
            using (var serviceHost = ServiceHost
                .Create(typeof(Program).Assembly)
                .WithDefaultConfigFile()
                .WithLogging()
                .WithApiVersionning()
                .WithSwagger("SocialNet Identity", "Veises SocuanNet Identity Service")
                .WithJwtAuth()
                .Build())
            {
                serviceHost.Run();
            }
        }
    }
}