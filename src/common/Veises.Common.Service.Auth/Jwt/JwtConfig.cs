using System;
using System.Text;
using JetBrains.Annotations;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtConfig
    {
        [NotNull]
        public string Audience { get; }

        [NotNull]
        public string Issuer { get; }

        [NotNull]
        public byte[] Key { get; }

        public JwtConfig([NotNull] string issuer, [NotNull] string audience, [NotNull] string key)
        {
            if (string.IsNullOrEmpty(issuer))
                throw new ArgumentException("JWT issuer is not defined.");
            if (string.IsNullOrEmpty(audience))
                throw new ArgumentException("JWT audience is not defined.");
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("JWT token key is not defined.");
            
            Issuer = issuer;
            Audience = audience;
            Key = Encoding.UTF8.GetBytes(key);
        }
    }
}
