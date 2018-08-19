using Veises.Common.Extensions;
using Veises.Common.Service;
using Veises.Common.Service.Middleware;
using Veises.Common.Service.Versionning;
using Veises.Common.Services.Swagger;

namespace Veises.SocialNet.Message
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var serviceHost = ServiceHost
                .Create(typeof(Program).Assembly.AsArray(), args)
                .WithDefaultConfigFile()
                .WithLogging()
                .WithApiVersionning()
                .WithSwagger("SocialNet Messages .", "Veises SocialNet message microservice.")
                .WithAssemblyDependencies(typeof(Program).Assembly)
                .WithRequestMiddleware<CurrentServerTimeRequestMiddleware>()
                .WithHttps(useForDev: true)
                .WithSessionId()
                .Build())
            {
                serviceHost.Run();
            }
        }
    }
}