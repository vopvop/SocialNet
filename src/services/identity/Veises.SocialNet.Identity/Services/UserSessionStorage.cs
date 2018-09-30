using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;
using Veises.Common.Service.IoC;
using Veises.SocialNet.Identity.Domain.UserSession;

namespace Veises.SocialNet.Identity.Services
{
    [InjectDependency(DependencyScope.Singleton)]
    internal sealed class UserSessionStorage
    {
        [NotNull]
        private readonly ConcurrentDictionary<Guid, UserSessionSet> _sessionSets;

        public UserSessionStorage()
        {
            _sessionSets = new ConcurrentDictionary<Guid, UserSessionSet>();
        }

        public void AddOrUpdate(Guid userId, Guid jwtTokenId)
        {
            _sessionSets.AddOrUpdate(userId,
                (id) => UserSessionSet.Create(userId),
                (id, sessionSet) =>
                {
                    sessionSet.AddOrUpdate(jwtTokenId);

                    return sessionSet;
                });
        }

        public void Validate(Guid userId, Guid jwtTokenId)
        {
            _sessionSets.AddOrUpdate(userId,
                (id) => UserSessionSet.Create(id),
                (id, sessionSet) =>
                {
                    sessionSet.Validate(jwtTokenId);
                    
                    return sessionSet;
                });
        }

        public void DropSession(Guid userId, Guid jwtTokenId)
        {
            _sessionSets.AddOrUpdate(
                userId,
                (id) => UserSessionSet.Create(id),
                (id, sessionSet) =>
                {
                    sessionSet.DropSession(jwtTokenId);
                    
                    return sessionSet;
                });
        }
    }
}