using JetBrains.Annotations;

namespace Veises.Common.Service.Auth.Jwt
{
    internal interface IJwtTokenProvider
    {
        [NotNull]
        string GetToken([NotNull] UserInfo userInfo);
    }
}
