using System;

using Microsoft.AspNetCore.Mvc;

using Veises.SocialNet.Identity.Api.V1.Models;

namespace Veises.SocialNet.Identity.Api.V1.Controllers
{
	/// <summary>
	/// User account controller
	/// </summary>
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[Produces("application/json", "application/xml")]
	public class AccountController: Controller
	{
		/// <summary>
		/// Login user
		/// </summary>
		/// <param name="loginUser">Login user model</param>
		[HttpPost]
		[Route("Login")]
		public User Login([FromBody]LoginUser loginUser)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Logoff user
		/// </summary>
		/// <param name="logoffUser">Logoff user model</param>
		[HttpPost]
		[Route("Logoff")]
		public void Logoff([FromBody]LogoffUser logoffUser)
		{
			throw new NotImplementedException();
		}
	}
}