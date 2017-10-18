using Veises.SocialNet.Identity.Contracts.Client;

namespace Veises.SocialNet.Frontend.Services.Identity
{
	public interface IIdentityServiceProvider
	{
		IVeisesUserIdentityAPI10 GetClient();
	}
}