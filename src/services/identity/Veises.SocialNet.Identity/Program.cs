using Veises.Common.Service;
using Veises.Common.Service.Auth;

namespace Veises.SocialNet.Identity
{
	public class Program
	{
		public static void Main()
		{
            using (var serviceHost = ServiceHost
                .Create(typeof(Program).Assembly)
                .WithDefaultConfigFile()
                .WithJwtAuth()
                .Build())
            {
                serviceHost.Run();
            }
        }
	}
}