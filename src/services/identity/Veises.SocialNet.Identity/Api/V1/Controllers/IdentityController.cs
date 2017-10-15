using System;

using Microsoft.AspNetCore.Mvc;

using Veises.SocialNet.Identity.Api.V1.Models;

namespace Veises.SocialNet.Identity.Api.V1.Controllers
{
	/// <summary>
	/// User identity controller
	/// </summary>
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[Produces("application/json", "application/xml")]
	public sealed class IdentityController: Controller
	{
		/// <summary>
		/// Get specified user identity
		/// </summary>
		/// <param name="id">Identity unique identificator</param>
		/// <returns>Requested user identity</returns>
		[HttpGet("{id}", Name = "Get")]
		public UserIdentity Get(string id)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Update user identity
		/// </summary>
		/// <param name="createIdentityModel">Create user identity information</param>
		[HttpPost]
		public UserIdentity Post([FromBody]CreateUserIdentity createIdentityModel)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Create user identity
		/// </summary>
		/// <param name="id">Identity unique identificator</param>
		/// <param name="updateUserIdentity">User identity information</param>
		[HttpPut("{id}")]
		public UserIdentity Put(string id, [FromBody]UpdateUserIdentity updateUserIdentity)
		{
			throw new NotImplementedException();
		}
	}
}