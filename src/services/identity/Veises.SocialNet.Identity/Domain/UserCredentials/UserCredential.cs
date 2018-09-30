using System;
using JetBrains.Annotations;
using Veises.Common.Extensions;

namespace Veises.SocialNet.Identity.Domain.UserCredentials
{
    internal sealed class UserCredential
    {
        private const int MinUserNameLength = 3;

        private readonly Guid _id;
        
        [NotNull]
        private readonly string _userLogin;

        [NotNull]
        private readonly string _passwordHash;

        private UserCredential(Guid id, [NotNull] string userLogin, [NotNull] string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(userLogin))
                throw new ArgumentException("User name is empty");
            
            if (userLogin.Length < MinUserNameLength)
                throw new ArgumentException($"User name is too short and less than {MinUserNameLength.Escaped()} character.");

            _id = id;
            _userLogin = userLogin;
            _passwordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }

        [NotNull]
        public static UserCredential Create([NotNull] string userLogin, [NotNull] string passwordHash)
        {
            var userId = Guid.NewGuid();
            
            return new UserCredential(userId, userLogin, passwordHash);
        }

        public Guid GetId()
        {
            return _id;
        }

        [NotNull]
        public string GetUserLogin()
        {
            return _userLogin;
        }

        public bool IsPasswordValid([NotNull] string passwordHash)
        {
            return _passwordHash.Equals(passwordHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}