using System.Text;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtConfig
    {
        public readonly string Audience;

        public readonly string Issuer;

        public readonly byte[] Key;

        public JwtConfig(string issuer, string audience, string key)
        {
            Issuer = issuer;
            Audience = audience;
            Key = Encoding.UTF8.GetBytes(key);
        }
    }
}
