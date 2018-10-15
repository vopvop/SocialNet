using JetBrains.Annotations;
using Veises.Common.Extensions;
using Veises.Common.Service;
using Veises.Common.Service.Auth;
using Veises.Common.Service.Versionning;
using Veises.Common.Service.Swagger;

namespace Veises.SocialNet.Identity
{
    public class Program
    {
        public static void Main([CanBeNull] string[] args)
        {
            using (var serviceHost = ServiceHost
                .Create(typeof(Program).Assembly.AsArray(), args)
                .WithDefaultConfigFile()
                .WithLogging()
                .WithApiVersionning()
                .WithSwagger("SocialNet Identity", "Veises Social Net Identity Service")
                .WithJwtAuth()
                .WithAssemblyDependencies(typeof(Program).Assembly)
                .WithHttps(useForDev: true)
                .WithSessionId()
                .Build())
            {
                serviceHost.Run();
            }
        }
    }
}