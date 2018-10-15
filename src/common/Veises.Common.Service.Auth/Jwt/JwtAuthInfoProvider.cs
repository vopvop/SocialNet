using System;
using JetBrains.Annotations;
using Veises.Common.Service.Utils;

namespace Veises.Common.Service.Auth.Jwt
{
    internal sealed class JwtAuthInfoProvider : IAuthInfoProvider
    {
        [NotNull]
        private readonly IHttpContextProvider _httpContextProvider;

        public JwtAuthInfoProvider([NotNull] IHttpContextProvider httpContextProvider)
        {
            _httpContextProvider = httpContextProvider ?? throw new ArgumentNullException(nameof(httpContextProvider));
        }

        public UserInfo GetAuthInfo()
        {
            var currentContext = _httpContextProvider.Get();
            
            var currentPrincipal = currentContext.User;
            
            if (currentPrincipal == null)
                throw new InvalidOperationException("Current user principal is not defined.");
            
            var jwtClaimsModel = JwtClaimsModel.Parse(currentPrincipal);

            return jwtClaimsModel.GetUserInfo();
        }
    }
}