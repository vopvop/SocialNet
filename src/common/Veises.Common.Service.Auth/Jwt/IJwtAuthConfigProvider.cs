using JetBrains.Annotations;

namespace Veises.Common.Service.Auth.Jwt
{
    internal interface IJwtAuthConfigProvider
    {
        [NotNull]
        JwtConfig GetConfig();
    }
}
