using Veises.SocialNet.Identity.Contracts.Client;

namespace Veises.SocialNet.Frontend.Services.Identity
{
	internal sealed class IdentityServiceProvider: IIdentityServiceProvider
	{
		public IVeisesUserIdentityAPI10 GetClient()
		{
			// TODO: initialize connection

			return new VeisesUserIdentityAPI10();
		}
	}
}