using JetBrains.Annotations;

namespace Veises.Common.Service.Auth
{
    public interface IAuthInfoProvider
    {
        [NotNull]
        UserInfo GetAuthInfo();
    }
}