using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;

namespace Veises.SocialNet.Identity.Domain.UserSession
{
    internal sealed class UserSessionSet
    {
        private readonly Guid _userId;

        [NotNull]
        private readonly ConcurrentDictionary<Guid, UserSession> _sessionSet;

        private UserSessionSet(Guid userId)
        {
            _userId = userId;
            
            _sessionSet = new ConcurrentDictionary<Guid, UserSession>();
        }

        [NotNull]
        public static UserSessionSet Create(Guid userId)
        {
            return new UserSessionSet(userId);
        }

        public void AddOrUpdate(Guid jwtTokenId)
        {
            _sessionSet.AddOrUpdate(
                jwtTokenId,
                (token) => UserSession.Create(jwtTokenId), 
                (tokenId, s) => UserSession.Create(jwtTokenId));
        }

        public Guid GetUserId()
        {
            return _userId;
        }

        public void Validate(Guid tokenId)
        {
            if (!_sessionSet.TryGetValue(tokenId, out var session))
                throw new ArgumentException("Invalid user session token Id.");
            
            session.Validate();
        }
    }
}