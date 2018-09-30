using System;
using JetBrains.Annotations;

namespace Veises.Common.Service.Auth
{
    public sealed class UserAuthData
    {
        public UserAuthData(Guid uid, [NotNull] string systemId)
        {
            SystemId = systemId ?? throw new ArgumentNullException(nameof(systemId));
            Uid = uid;
        }

        public Guid Uid { get; }

        [NotNull]
        public string SystemId { get; }
    }
}