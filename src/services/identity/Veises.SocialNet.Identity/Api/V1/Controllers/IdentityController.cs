using System;
using Microsoft.AspNetCore.Mvc;
using Veises.SocialNet.Identity.Api.V1.Models;

namespace Veises.SocialNet.Identity.Api.V1.Controllers
{
    /// <summary>
    /// User identity controller
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json", "application/xml")]
    public sealed class IdentityController : Controller
    {
        /// <summary>
        /// Get specified user identity
        /// </summary>
        /// <remarks>
        /// Get user by a unique identifier.
        /// </remarks>
        /// <param name="id">Identity unique identificator</param>
        /// <returns>Requested user identity</returns>
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(UserIdentity), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Get(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create new user identity
        /// </summary>
        /// <remarks>
        /// Create new user identity
        /// </remarks>
        /// <param name="createIdentityModel">Create user identity information</param>
        [HttpPost]
        [ProducesResponseType(typeof(UserIdentity), 201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] CreateUserIdentity createIdentityModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Upate user identity
        /// </summary>
        /// <remarks>
        /// Update exists user identity
        /// </remarks>
        /// <param name="id">Identity unique identificator</param>
        /// <param name="updateUserIdentity">User identity information</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserIdentity), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(string id, [FromBody] UpdateUserIdentity updateUserIdentity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            throw new NotImplementedException();
        }
    }
}