using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Veises.Common.Service.Middleware;
using Veises.Common.Service.Settings;

namespace Veises.Common.Service.Utils
{
    internal sealed class SessionIdRequestMiddleware : IRequestMiddleware
    {
        [NotNull]
        private readonly SessionIdProvider _sessionIdProvider;

        private readonly HttpSessionSettings _httpSessionSettings;

        public SessionIdRequestMiddleware(
            [NotNull] SessionIdProvider sessionIdProvider,
            [NotNull] ISetting<HttpSessionSettings> httpSessionSettings)
        {
            if (httpSessionSettings == null) throw new ArgumentNullException(nameof(httpSessionSettings));
            _sessionIdProvider = sessionIdProvider ?? throw new ArgumentNullException(nameof(sessionIdProvider));

            _httpSessionSettings = httpSessionSettings.GetSettings();
        }

        public Task<bool> ExecuteAsync(HttpContext httpContext)
        {
            var sessionId = httpContext.Request.Headers.ContainsKey(_httpSessionSettings.HeaderName)
                ? (string) httpContext.Request.Headers[_httpSessionSettings.HeaderName]
                : SessionIdBuilder.Build();

            httpContext.Response.Headers.Add(_httpSessionSettings.HeaderName, sessionId);
            
            _sessionIdProvider.SetSessionId(sessionId);

            return Task.FromResult(true);
        }
    }
}