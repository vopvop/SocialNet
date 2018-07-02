using Microsoft.AspNetCore.Http;
using System;

namespace Veises.Common.Service.Utils
{
    internal sealed class HttpContextProvider : IHttpContextProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public HttpContext Get() => _httpContextAccessor.HttpContext;
    }
}