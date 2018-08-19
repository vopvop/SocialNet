using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Veises.Common.Service.Middleware;

namespace Veises.Common.Service.Utils
{
    internal sealed class SessionIdRequestMiddleware : IRequestMiddleware
    {
        private const string SessionIdHeaderName = "sn-session-id";
        
        private readonly SessionIdProvider _sessionIdProvider;

        public SessionIdRequestMiddleware(SessionIdProvider sessionIdProvider)
        {
            _sessionIdProvider = sessionIdProvider ?? throw new ArgumentNullException(nameof(sessionIdProvider));
        }

        public Task<bool> ExecuteAsync(HttpContext httpContext)
        {
            var sessionId = string.Empty;
            
            if (httpContext.Request.Headers.ContainsKey(SessionIdHeaderName))
            {
                sessionId = httpContext.Request.Headers[SessionIdHeaderName];
            }
            else
            {
                sessionId = SessionIdBuilder.Build();
            }
            
            httpContext.Response.Headers.Add(SessionIdHeaderName, sessionId);
            
            _sessionIdProvider.SetSessionId(sessionId);

            return Task.FromResult(true);
        }
    }
}