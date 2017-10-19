using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest;

using Veises.SocialNet.Frontend.Models.Identity;
using Veises.SocialNet.Frontend.Services;
using Veises.SocialNet.Frontend.Services.Identity;
using Swashbuckle.AspNetCore.SwaggerGen;

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

		private readonly IIdentityServiceProvider _identityServiceProvider;

		/// <summary>
		/// Identity controller constructor
		/// </summary>
		/// <param name="sessionProvider">User sessionn provider</param>
		/// <param name="identityServiceProvider">User identity API client provider</param>
		public IdentityController(
			ISessionProvider sessionProvider,
			IIdentityServiceProvider identityServiceProvider)
		{
			_sessionProvider = sessionProvider ?? throw new ArgumentNullException(nameof(sessionProvider));
			_identityServiceProvider = identityServiceProvider ?? throw new ArgumentNullException(nameof(identityServiceProvider));
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

			if (!session.TryGetUserId(out string userUid))
			{
				return Unauthorized();
			}

			using (var identityService = _identityServiceProvider.GetClient())
			{
				try
				{
					var userIdentity = identityService.ApiV1IdentityByIdGetWithHttpMessagesAsync(userUid).Result;

					var userModel = new UserModel
					{
						DisplauName = userIdentity.Body.UserName
					};

					return Ok(userModel);
				}
				catch (HttpOperationException e)
				{
					return new StatusCodeResult(523);
				}
				catch (SerializationException e)
				{
					return new UnsupportedMediaTypeResult();
				}
			}
		}
	}
}