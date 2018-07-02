namespace Veises.Common.Service.Auth.Jwt
{
    internal interface IJwtTokenProvider
    {
        string GetToken(string userSystemName);
    }
}
