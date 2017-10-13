using System.ComponentModel.DataAnnotations;

namespace Veises.SocialNet.Identity.Models
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