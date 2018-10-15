using System;
using JetBrains.Annotations;
using Veises.Common.Service.Settings;
using Veises.Common.Service.Utils;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtAuthService: IAuthService
    {
        [NotNull]
        private readonly IHttpContextProvider _httpContextProvider;

        [NotNull]
        private readonly IJwtTokenProvider _jwtTokenProvider;

        [NotNull]
        private readonly JwtHttpSettings _jwtHttpSettings;

        public JwtAuthService(
            [NotNull] IJwtTokenProvider jwtTokenProvider,
            [NotNull] IHttpContextProvider httpContextProvider,
            [NotNull] ISetting<JwtHttpSettings> jwtHttpSettings)
        {
            if (jwtHttpSettings == null) throw new ArgumentNullException(nameof(jwtHttpSettings));
            _httpContextProvider = httpContextProvider ?? throw new ArgumentNullException(nameof(httpContextProvider));
            _jwtTokenProvider = jwtTokenProvider ?? throw new ArgumentNullException(nameof(jwtTokenProvider));
            _jwtHttpSettings = jwtHttpSettings.GetSettings();
        }

        public AuthSession Authorize(UserAuthData userAuthData)
        {
            if (userAuthData == null)
                throw new ArgumentNullException(nameof(userAuthData));

            var jwtToken = _jwtTokenProvider.GetToken(userAuthData);

            _httpContextProvider
                .Get()
                .Response
                .Headers
                .Add(_jwtHttpSettings.HeaderName, jwtToken.TokenValue);
            
            return new AuthSession(jwtToken.TokenId);
        }
    }
}