using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Veises.Common.Service.Utils;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtAuthService: IAuthService
    {
        private const string JwtTokenHeaderName = "jwt-token";

        private readonly IUserCredentialsValidator _userCredentialsValidator;

        private readonly IHttpContextProvider _httpContextProvider;

        private readonly IJwtTokenProvider _jwtTokenProvider;

        public JwtAuthService(IJwtTokenProvider jwtTokenProvider, IHttpContextProvider httpContextProvider)
        {
            _httpContextProvider = httpContextProvider ?? throw new ArgumentNullException(nameof(httpContextProvider));
            _jwtTokenProvider = jwtTokenProvider ?? throw new ArgumentNullException(nameof(jwtTokenProvider));
        }

        public void Authorize(IUserAuthData userAuthData)
        {
            if (userAuthData == null)
                throw new ArgumentNullException(nameof(userAuthData));

            if (!_userCredentialsValidator.IsValid(userAuthData))
                throw new UnauthorizedAccessException();

            var jwtToken = _jwtTokenProvider.GetToken(userAuthData.GetUserSystemName());

            _httpContextProvider.Get().Response.Headers.Add(JwtTokenHeaderName, jwtToken);
        }

        public UserInfo GetUserInfo()
        {
            var currentUser = _httpContextProvider.Get().User;

            var userLoginClaim = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var userSystemName = currentUser.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

            return new UserInfo(userSystemName);
        }
    }
}
