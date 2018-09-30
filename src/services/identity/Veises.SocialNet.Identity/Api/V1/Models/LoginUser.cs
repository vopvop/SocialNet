using System.ComponentModel.DataAnnotations;

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
        [Required]
        [MinLength(ModelRestrictions.UserNameMinLength)]
        [MaxLength(ModelRestrictions.UserNameMaxLength)]
        public string Login { get; set; }

        /// <summary>
        /// User password hash
        /// </summary>
        public string PasswordHash { get; set; }
    }
}