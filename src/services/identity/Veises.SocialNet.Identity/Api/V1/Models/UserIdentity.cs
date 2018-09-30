using System.ComponentModel.DataAnnotations;

namespace Veises.SocialNet.Identity.Api.V1.Models
{
    /// <summary>
    /// User identity model
    /// </summary>
    public sealed class UserIdentity
    {
        /// <summary>
        /// User display name
        /// </summary>
        [Required]
        [MinLength(ModelRestrictions.UserNameMinLength)]
        [MaxLength(ModelRestrictions.UserNameMaxLength)]
        public string Login { get; set; }

        /// <summary>
        /// User unique identifier
        /// </summary>
        [Required]
        public UserUidInfo UserUidInfo { get; set; }
    }
}