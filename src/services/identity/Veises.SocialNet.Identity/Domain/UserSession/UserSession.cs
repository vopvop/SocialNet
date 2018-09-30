using System;
using JetBrains.Annotations;

namespace Veises.SocialNet.Identity.Domain.UserSession
{
    internal sealed class UserSession
    {
        private readonly Guid _tokenId;

        private UserSession(Guid tokenId)
        {
            _tokenId = tokenId;
        }

        [NotNull]
        public static UserSession Create(Guid jwtTokenId)
        {
            return new UserSession(jwtTokenId);
        }

        public Guid GetTokenId()
        {
            return _tokenId;
        }

        public void Validate()
        {
            // do nothing
        }
    }
}