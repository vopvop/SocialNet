using System;
using JetBrains.Annotations;
using Veises.Common.Service.Auth;
using Veises.Common.Service.IoC;
using Veises.SocialNet.Identity.Api.V1.Models;

namespace Veises.SocialNet.Identity.Services
{
    [InjectDependency(DependencyScope.Singleton)]
    internal sealed class IdentityService : IIdentityService
    {
        [NotNull]
        private readonly IAuthService _authService;

        [NotNull]
        private readonly UserCredentialStorage _userCredentialStorage;

        public IdentityService([NotNull] IAuthService authService, [NotNull] UserCredentialStorage userCredentialStorage)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _userCredentialStorage = userCredentialStorage ?? throw new ArgumentNullException(nameof(userCredentialStorage));
        }

        public bool TryGetCurrent(out UserIdentity userIdentity)
        {
            var currentUserInfo = _authService.GetUserInfo();

            var userCredential = _userCredentialStorage.Get(currentUserInfo.Login);

            userIdentity = new UserIdentity
            {
                Login = userCredential.GetUserLogin(),
                UserUidInfo = new UserUidInfo
                {
                    Uid = userCredential.GetId()
                }
            };

            return true;
        }

        public bool TryAuthorize(string userName, string passwordHash)
        {
            var userCredential = _userCredentialStorage.Get(userName);

            if (!userCredential.IsPasswordValid(passwordHash))
                return false;

            _authService.Authorize(
                new UserAuthData(
                    userCredential.GetId(),
                    userCredential.GetUserLogin()));

            return true;
        }
    }
}