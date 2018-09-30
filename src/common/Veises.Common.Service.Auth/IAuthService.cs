using JetBrains.Annotations;

namespace Veises.Common.Service.Auth
{
    public interface IAuthService
    {
        [NotNull]
        UserInfo GetUserInfo();

        AuthSession Authorize([NotNull] UserAuthData userAuthData);
    }
}