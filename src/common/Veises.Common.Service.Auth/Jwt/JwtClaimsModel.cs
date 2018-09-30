using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using JetBrains.Annotations;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtClaimsModel
    {
        private readonly Guid _userid;

        [NotNull]
        private readonly string _login;

        private JwtClaimsModel(Guid userId, [NotNull] string login)
        {
            _login = login ?? throw new ArgumentNullException(nameof(login));
            _userid = userId;
        }

        [NotNull]
        public static JwtClaimsModel Create([NotNull] UserInfo userInfo)
        {
            if (userInfo == null) throw new ArgumentNullException(nameof(userInfo));
            
            return new JwtClaimsModel(userInfo.Uid, userInfo.Login);
        }

        [NotNull]
        public static JwtClaimsModel Parse([NotNull] ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null) throw new ArgumentNullException(nameof(claimsPrincipal));

            if (claimsPrincipal.HasClaim(c => c.Type == JwtRegisteredClaimNames.Jti) &&
                claimsPrincipal.HasClaim(c => c.Type == ClaimTypes.Name))
            {
                var userIdValue = claimsPrincipal.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
                var login = claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.Name).Value;

                if (!Guid.TryParse(userIdValue, out var userId))
                {
                    throw new ArgumentException("User id is incorrect.");
                }
                
                return new JwtClaimsModel(userId, login);
            }
            
            throw new ArgumentException("Invalid claims content.");
        }

        [NotNull]
        public IEnumerable<Claim> GetClaims()
        {
            yield return new Claim(JwtRegisteredClaimNames.Jti, _userid.ToString("N"));
            yield return new Claim(JwtRegisteredClaimNames.UniqueName, _login);
        }

        [NotNull]
        public UserInfo GetUserInfo()
        {
            return new UserInfo(_login, _userid);
        }
    }
}