using System;

using Microsoft.AspNetCore.Http;

namespace Veises.SocialNet.Frontend.Services
{
	/// <summary>
	/// User session abstraction
	/// </summary>
	public sealed class UserSession
	{
		private readonly ISession _session;

		private const string UserIdKey = "user-id";

		private const string UserAuthenticatedKey = "user-auth";

		private UserSession(ISession session)
		{
			_session = session ?? throw new ArgumentNullException(nameof(session));
		}

		/// <summary>
		/// Create user session from HTTP context
		/// </summary>
		/// <param name="httpContext">HTTP context definnition</param>
		/// <returns>User session</returns>
		public static UserSession FromHttpContext(HttpContext httpContext) => new UserSession(httpContext.Session);

		/// <summary>
		/// Get user Unique Identifier from session
		/// </summary>
		/// <param name="userId">User unique identifier</param>
		/// <returns>User identifier exists</returns>
		public bool TryGetUserId(out string userId)
		{
			userId = _session.GetString(UserIdKey);

			return userId != null;
		}
	}
}