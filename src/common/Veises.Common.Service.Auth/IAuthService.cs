using JetBrains.Annotations;

namespace Veises.Common.Service.Auth
{
    public interface IAuthService
    {
        [NotNull]
        UserInfo GetUserInfo();

        void Authorize([NotNull] UserAuthData userAuthData);
    }
}