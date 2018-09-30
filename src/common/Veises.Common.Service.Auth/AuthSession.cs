using System;

namespace Veises.Common.Service.Auth
{
    public sealed class AuthSession
    {
        public AuthSession(Guid sessionId)
        {
            SessionId = sessionId;
        }

        public Guid SessionId { get; }
    }
}