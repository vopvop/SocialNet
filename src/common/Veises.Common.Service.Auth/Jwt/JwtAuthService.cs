using System;
using JetBrains.Annotations;
using Veises.Common.Service.Utils;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtAuthService: IAuthService
    {
        private const string JwtTokenHeaderName = "jwt-token";

        [NotNull]
        private readonly IHttpContextProvider _httpContextProvider;

        [NotNull]
        private readonly IJwtTokenProvider _jwtTokenProvider;

        public JwtAuthService(
            [NotNull] IJwtTokenProvider jwtTokenProvider,
            [NotNull] IHttpContextProvider httpContextProvider)
        {
            _httpContextProvider = httpContextProvider ?? throw new ArgumentNullException(nameof(httpContextProvider));
            _jwtTokenProvider = jwtTokenProvider ?? throw new ArgumentNullException(nameof(jwtTokenProvider));
        }

        public void Authorize(UserAuthData userAuthData)
        {
            if (userAuthData == null)
                throw new ArgumentNullException(nameof(userAuthData));

            var jwtToken = _jwtTokenProvider.GetToken(
                new UserInfo(
                    userAuthData.SystemId,
                    userAuthData.Uid));

            _httpContextProvider
                .Get()
                .Response
                .Headers
                .Add(JwtTokenHeaderName, jwtToken);
        }

        public UserInfo GetUserInfo()
        {
            var currentUser = _httpContextProvider.Get().User;
            
            if (currentUser == null)
                throw new InvalidOperationException("Current user principal is not defined.");

            var jwtClaimsModel = JwtClaimsModel.Parse(currentUser);

            return jwtClaimsModel.GetUserInfo();
        }
    }
}
