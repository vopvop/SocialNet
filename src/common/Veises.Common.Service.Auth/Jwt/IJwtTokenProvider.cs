using JetBrains.Annotations;

namespace Veises.Common.Service.Auth.Jwt
{
    internal interface IJwtTokenProvider
    {
        [NotNull]
        JwtToken GetToken([NotNull] UserAuthData userAuthData);
    }
}
