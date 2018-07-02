using System;

namespace Veises.Common.Service.Auth
{
    public sealed class UserInfo
    {
        public string SystemName { get; }

        public UserInfo(string systemName)
        {
            SystemName = systemName ?? throw new ArgumentNullException(nameof(systemName));
        }
    }
}