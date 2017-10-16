using System.ComponentModel.DataAnnotations;

namespace Veises.SocialNet.Identity.Api.V1.Models
{
	/// <summary>
	/// User identity model
	/// </summary>
	public sealed class UserIdentity
	{
		/// <summary>
		/// Describes user identity is blocked
		/// </summary>
		[Required]
		public bool IsBlocked { get; set; }

		/// <summary>
		/// User display name
		/// </summary>
		[Required]
		[MinLength(ModelRestrictions.UserNameMinLength)]
		[MaxLength(ModelRestrictions.UserNameMaxLength)]
		public string UserName { get; set; }

		/// <summary>
		/// User unique identifier
		/// </summary>
		[Required]
		public UserUid UserUid { get; set; }
	}
}