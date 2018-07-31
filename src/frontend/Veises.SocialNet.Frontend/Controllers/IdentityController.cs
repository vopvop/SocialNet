using System;

using Microsoft.AspNetCore.Mvc;

using Veises.SocialNet.Frontend.Models.Identity;
using Veises.SocialNet.Frontend.Services;

namespace Veises.SocialNet.Frontend.Controllers
{
	/// <summary>
	/// User identity controller
	/// </summary>
	/// <remarks>
	/// Manage user identity and so
	/// </remarks>
	[Produces("application/json")]
	[Route("api/Identity")]
	public sealed class IdentityController: Controller
	{
		private readonly ISessionProvider _sessionProvider;

		/// <summary>
		/// Identity controller constructor
		/// </summary>
		/// <param name="sessionProvider">User sessionn provider</param>
		public IdentityController(			ISessionProvider sessionProvider)
		{
			_sessionProvider = sessionProvider ?? throw new ArgumentNullException(nameof(sessionProvider));
		}

		/// <summary>
		/// Get current user identity
		/// </summary>
		[HttpGet]
		[Route("Current")]
		[ProducesResponseType(typeof(UserModel), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(415)]
		[ProducesResponseType(523)]
		public IActionResult GetCurrent()
		{
			var session = _sessionProvider.GetSession();

			if (!session.TryGetUserId(out var userUid))
			{
				return Unauthorized();
			}

            throw new NotImplementedException();
		}
	}
}