using System.ComponentModel.DataAnnotations;

namespace Veises.SocialNet.Identity.Api.V1.Models
{
	/// <summary>
	/// New user identity model
	/// </summary>
	public sealed class CreateUserIdentity
	{
		/// <summary>
		/// User name
		/// </summary>
		[Required]
		public string UserName { get; set; }
	}
}