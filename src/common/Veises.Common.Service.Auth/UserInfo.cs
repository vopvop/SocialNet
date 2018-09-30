using System;
using JetBrains.Annotations;

namespace Veises.Common.Service.Auth
{
    public sealed class UserInfo
    {
        [NotNull]
        public string Login { get; }
        
        public Guid Uid { get; }

        public UserInfo([NotNull] string login, Guid uid)
        {
            Login = login ?? throw new ArgumentNullException(nameof(login));
            
            Uid = uid;
        }
    }
}