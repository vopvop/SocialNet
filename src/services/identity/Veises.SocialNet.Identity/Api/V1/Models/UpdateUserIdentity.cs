using System.ComponentModel.DataAnnotations;

namespace Veises.SocialNet.Identity.Api.V1.Models
{
	/// <summary>
	/// User identity
	/// </summary>
	public sealed class UpdateUserIdentity
	{
		/// <summary>
		/// User display name
		/// </summary>
		[Required]
		[MinLength(ModelRestrictions.UserNameMinLength)]
		[MaxLength(ModelRestrictions.UserNameMaxLength)]
		public string UserName { get; set; }
	}
}