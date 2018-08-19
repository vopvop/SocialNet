using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Veises.Common.Service.Utils;

namespace Veises.Common.Service.Middleware
{
    public sealed class CurrentServerTimeRequestMiddleware : IRequestMiddleware
    {
        private const string ServerTime = "server-time-utc";

        private readonly ITimeService _timeService;

        public CurrentServerTimeRequestMiddleware(ITimeService timeService)
        {
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
        }

        public Task<bool> ExecuteAsync(HttpContext httpContext)
        {
            var serverTimeUtc = _timeService.GetCurrentUtc();
            
            httpContext.Response.Headers.Add(ServerTime, serverTimeUtc.ToString("u"));
            
            return Task.FromResult(true);
        }
    }
}