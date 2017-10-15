namespace Veises.SocialNet.Identity.Api.V1.Models
{
	/// <summary>
	/// Login user model
	/// </summary>
	public sealed class LoginUser
	{
		/// <summary>
		/// User name
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// User password hash
		/// </summary>
		public string PasswordHash { get; set; }
	}
}