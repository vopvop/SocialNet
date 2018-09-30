using JetBrains.Annotations;
using Veises.SocialNet.Identity.Api.V1.Models;

namespace Veises.SocialNet.Identity.Services
{
    public interface IIdentityService
    {
        bool TryGetCurrent([NotNull] out UserIdentity userIdentity);

        bool TryAuthorize([NotNull] string userName, [NotNull] string passwordHash);

        void Logout();
    }
}