namespace Veises.Common.Service.Auth.Jwt
{
    internal interface IJwtAuthConfigProvider
    {
        JwtConfig GetConfig();
    }
}
