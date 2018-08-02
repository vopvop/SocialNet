using Veises.Common.Service;
using Veises.Common.Service.Auth;
using Veises.Common.Service.Versionning;
using Veises.Common.Services.Swagger;

namespace Veises.SocialNet.Identity
{
	public sealed class Program
	{
		public static void Main()
		{
            using (var serviceHost = ServiceHost
                .Create(typeof(Program).Assembly)
                .WithDefaultConfigFile()
	            .WithLogging()
                .WithJwtAuth()
	            .WithApiVersionning()
				.WithSwagger("Veises Identity", "Veises Social Network identity service.")
                .Build())
            {
                serviceHost.Run();
            }
        }
	}
}