using System;
using Microsoft.Extensions.Configuration;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtAuthConfigProvider : IJwtAuthConfigProvider
    {
        private const string JwtIssuerConfigName = "Jwt:Issuer";

        private const string JwtAudienceConfigName = "Jwt:Audience";

        private const string JwtKeyConfigName = "Jwt:Key";

        private readonly IConfiguration _configuration;

        public JwtAuthConfigProvider(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public JwtConfig GetConfig()
        {
            return new JwtConfig(
                _configuration[JwtIssuerConfigName],
                _configuration[JwtAudienceConfigName],
                _configuration[JwtKeyConfigName]);
        }
    }
}
