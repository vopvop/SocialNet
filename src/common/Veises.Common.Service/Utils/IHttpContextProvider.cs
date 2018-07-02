using Microsoft.AspNetCore.Http;

namespace Veises.Common.Service.Utils
{
    public interface IHttpContextProvider
    {
        HttpContext Get();
    }
}