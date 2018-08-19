using System;

namespace Veises.Common.Service.Utils
{
    internal sealed class SessionInfoProvider : ISessionInfoProvider
    {
        private readonly SessionIdProvider _sessionIdProvider;

        public SessionInfoProvider(SessionIdProvider sessionIdProvider)
        {
            _sessionIdProvider = sessionIdProvider ?? throw new ArgumentNullException(nameof(sessionIdProvider));
        }

        public string GetSessionId()
        {
            return _sessionIdProvider.GetSessionId();
        }
    }
}