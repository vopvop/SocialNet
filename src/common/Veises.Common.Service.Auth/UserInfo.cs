using System;
using JetBrains.Annotations;

namespace Veises.Common.Service.Auth
{
    public sealed class UserInfo
    {
        [NotNull]
        public string Login { get; }
        
        public Guid Uid { get; }
        
        public Guid SessionId { get; }

        public UserInfo([NotNull] string login, Guid uid, Guid sessionId)
        {
            Login = login ?? throw new ArgumentNullException(nameof(login));
            
            Uid = uid;
            SessionId = sessionId;
        }
    }
}