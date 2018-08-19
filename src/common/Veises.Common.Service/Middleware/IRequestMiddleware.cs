using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Veises.Common.Service.Middleware
{
    public interface IRequestMiddleware
    {
        Task<bool> ExecuteAsync(HttpContext httpContext);
    }
}