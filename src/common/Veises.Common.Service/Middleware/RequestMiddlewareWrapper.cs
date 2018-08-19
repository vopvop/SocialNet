using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Veises.Common.Service.Middleware
{
    internal sealed class RequestMiddlewareWrapper<TRequestMiddleware>
        where TRequestMiddleware : class, IRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddlewareWrapper(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext cotext, TRequestMiddleware middleware)
        {
            if (cotext == null) throw new ArgumentNullException(nameof(cotext));
            if (middleware == null) throw new ArgumentNullException(nameof(middleware));

            var processNext = await middleware.ExecuteAsync(cotext);
            
            if (processNext == false)
                return;

            if (_next != null)
                await _next(cotext);
        }
    }
}