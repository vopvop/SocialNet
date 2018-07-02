using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtTokenProvider: IJwtTokenProvider
    {
        private readonly IJwtAuthConfigProvider _jwtAuthConfigProvider;

        public JwtTokenProvider(IJwtAuthConfigProvider jwtAuthConfigProvider)
        {
            _jwtAuthConfigProvider = jwtAuthConfigProvider ?? throw new ArgumentNullException(nameof(jwtAuthConfigProvider));
        }

        public string GetToken(string userSystemName)
        {
            if (userSystemName == null)
                throw new ArgumentNullException(nameof(userSystemName));

            var jwtConfig = _jwtAuthConfigProvider.GetConfig();

            var key = new SymmetricSecurityKey(jwtConfig.Key);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, userSystemName, ClaimValueTypes.String)
            };

            var token = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
