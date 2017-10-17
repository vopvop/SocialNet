using Microsoft.AspNetCore.Mvc;

namespace Veises.SocialNet.Frontend.Controllers
{
	[Produces("application/json")]
	[Route("api/Identity")]
	public sealed class IdentityController: Controller
	{
		private const string userIdKey = "userUid";

		/// <summary>
		/// Get current user identity
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("Current")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult GetCurrent()
		{
			// TODO: validate cookie and user

			return NotFound();
		}
	}
}