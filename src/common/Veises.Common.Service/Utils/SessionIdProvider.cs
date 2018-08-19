using System;

namespace Veises.Common.Service.Utils
{
    internal sealed class SessionIdProvider
    {
        private string _sessionId = "unknown";

        public void SetSessionId(string sessionId)
        {
            _sessionId = sessionId ?? throw new ArgumentNullException(nameof(sessionId));
        }

        public string GetSessionId()
        {
            return _sessionId;
        }
    }
}