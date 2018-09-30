using System.ComponentModel.DataAnnotations;

namespace Veises.SocialNet.Identity.Api.V1.Models
{
    /// <summary>
    /// User information
    /// </summary>
    public sealed class UserInfo
    {
        /// <summary>
        /// User identity information
        /// </summary>
        [Required]
        public UserIdentity Identity { get; set; }
    }
}