using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using JetBrains.Annotations;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtTokenProvider: IJwtTokenProvider
    {
        [NotNull]
        private readonly IJwtAuthConfigProvider _jwtAuthConfigProvider;

        public JwtTokenProvider([NotNull] IJwtAuthConfigProvider jwtAuthConfigProvider)
        {
            _jwtAuthConfigProvider = jwtAuthConfigProvider ?? throw new ArgumentNullException(nameof(jwtAuthConfigProvider));
        }

        public JwtToken GetToken(UserAuthData userAuthData)
        {
            if (userAuthData == null)
                throw new ArgumentNullException(nameof(userAuthData));

            var jwtConfig = _jwtAuthConfigProvider.GetConfig();

            var key = new SymmetricSecurityKey(jwtConfig.Key);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claimsModel = JwtClaimsModel.Create(userAuthData);

            var token = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claimsModel.GetClaims(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            
            return new JwtToken(claimsModel.GetTokenId(), tokenValue);
        }
    }
}
