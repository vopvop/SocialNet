using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace Veises.Common.Service.Middleware
{
    [UsedImplicitly]
    internal sealed class RequestMiddlewareWrapper<TRequestMiddleware>
        where TRequestMiddleware : class, IRequestMiddleware
    {
        [NotNull]
        private readonly RequestDelegate _next;

        public RequestMiddlewareWrapper([NotNull] RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(
            [NotNull] HttpContext httpContext, 
            [NotNull] TRequestMiddleware requestMiddleware)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (requestMiddleware == null) throw new ArgumentNullException(nameof(requestMiddleware));

            var processNext = await requestMiddleware.ExecuteAsync(httpContext);
            
            if (processNext == false)
                return;

            if (_next != null)
                await _next(httpContext);
        }
    }
}