using System;

using Microsoft.AspNetCore.Http;

namespace Veises.SocialNet.Frontend.Services
{
	internal sealed class SessionProvider: ISessionProvider
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SessionProvider(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
		}

		public UserSession GetSession()
		{
			return UserSession.FromHttpContext(_httpContextAccessor.HttpContext);
		}
	}
}