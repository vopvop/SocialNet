using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Veises.SocialNet.Identity.Api.V1.Models;

namespace Veises.SocialNet.Identity.Api.V1.Controllers
{
	/// <summary>
	/// User identity controller
	/// </summary>
	[Produces("application/json", "application/xml")]
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public sealed class IdentityController: Controller
	{
		/// <summary>
		/// Get all user identities
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<UserIdentity> Get()
		{
			return new[] { new UserIdentity { UserName = "value1" }, new UserIdentity { UserName = "value2" } };
		}

		/// <summary>
		/// Get specified user identity
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}", Name = "Get")]
		public string Get(int id)
		{
			return "value";
		}

		/// <summary>
		/// Update user identity
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public void Post([FromBody]CreateUserIdentity createIdentityModel)
		{
		}

		/// <summary>
		/// Create user identity
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		/// <summary>
		/// Delete user identity
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
