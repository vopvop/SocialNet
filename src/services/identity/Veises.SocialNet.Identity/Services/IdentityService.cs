using System;
using JetBrains.Annotations;
using Veises.Common.Logging;
using Veises.Common.Service.Auth;
using Veises.Common.Service.IoC;
using Veises.SocialNet.Identity.Api.V1.Models;

namespace Veises.SocialNet.Identity.Services
{
    [UsedImplicitly]
    [InjectDependency(DependencyScope.Singleton)]
    internal sealed class IdentityService : IIdentityService
    {
        private static readonly ILog Log = LogProvider.GetLogFor<IdentityService>();
        
        [NotNull] private readonly IAuthService _authService;

        [NotNull] private readonly UserCredentialStorage _userCredentialStorage;

        [NotNull] private readonly UserSessionStorage _userSessionStorage;

        public IdentityService(
            [NotNull] IAuthService authService,
            [NotNull] UserCredentialStorage userCredentialStorage,
            [NotNull] UserSessionStorage userSessionStorage)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _userCredentialStorage = userCredentialStorage ?? throw new ArgumentNullException(nameof(userCredentialStorage));
            _userSessionStorage = userSessionStorage ?? throw new ArgumentNullException(nameof(userSessionStorage));
        }

        public bool TryGetCurrent(out UserIdentity userIdentity)
        {
            var currentUserInfo = _authService.GetUserInfo();

            var userCredential = _userCredentialStorage.Get(currentUserInfo.Login);
            
            _userSessionStorage.Validate(currentUserInfo.Uid, currentUserInfo.SessionId);

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
            {
                return false;
            }

            var authSession = _authService.Authorize(
                new UserAuthData(
                    userCredential.GetId(),
                    userCredential.GetUserLogin()));
            
            _userSessionStorage.AddOrUpdate(userCredential.GetId(), authSession.SessionId);
            
            Log.LogInfo($"User authenticated. Login='{userCredential.GetUserLogin()}', Uid='{userCredential.GetId()}'.");

            return true;
        }

        public void Logout()
        {
            var currentUserAuthInfo = _authService.GetUserInfo();
            
            _userSessionStorage.DropSession(currentUserAuthInfo.Uid, currentUserAuthInfo.SessionId);
            
            Log.LogInfo($"User logged out. Login='{currentUserAuthInfo.Login}', Uid='{currentUserAuthInfo.Uid}'.");
        }
    }
}