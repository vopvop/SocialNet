using System;
using System.Net;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veises.SocialNet.Identity.Api.V1.Models;
using Veises.SocialNet.Identity.Services;

namespace Veises.SocialNet.Identity.Api.V1.Controllers
{
    /// <summary>
    /// User identity controller
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json", "application/xml")]
    public sealed class IdentityController : Controller
    {
        [NotNull]
        private readonly IIdentityService _identityService;

        public IdentityController([NotNull] IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        /// <summary>
        /// Get current user Identity info.
        /// </summary>
        /// <returns>User info.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserIdentity), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [UsedImplicitly]
        public IActionResult Get()
        {
            if (!_identityService.TryGetCurrent(out var currentIdentity))
            {
                return Forbid();
            }
            
            return Ok(currentIdentity);
        }

        /// <summary>
        /// Authorize user.
        /// </summary>
        /// <remarks>
        /// Authorize user by login and password.
        /// </remarks>
        /// <returns>Authorization status (success or not).</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.Forbidden)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [UsedImplicitly]
        public IActionResult Post([NotNull][FromBody] LoginUser loginUser)
        {
            if (loginUser == null) throw new ArgumentNullException(nameof(loginUser));
            
            if (!ModelState.IsValid)
            {
                return BadRequest("Authentication data is invalid");
            }

            if (_identityService.TryAuthorize(loginUser.Login, loginUser.PasswordHash))
            {
                return NoContent();
            }

            return Forbid();
        }
    }
}