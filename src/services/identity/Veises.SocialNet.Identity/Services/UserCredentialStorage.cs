using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;
using Veises.Common.Service.IoC;
using Veises.SocialNet.Identity.Domain.UserCredentials;

namespace Veises.SocialNet.Identity.Services
{
    [InjectDependency(DependencyScope.Singleton)]
    internal sealed class UserCredentialStorage
    {
        [NotNull]
        private readonly ConcurrentDictionary<string, UserCredential> _userCredentials;

        public UserCredentialStorage()
        {
            _userCredentials = new ConcurrentDictionary<string, UserCredential>();
            
            Add("user.login.1", "123321");
            Add("user.login.2", "321123");
        }

        public void Add([NotNull] string userLogin, [NotNull] string passwordHash)
        {
            var userCredential = UserCredential.Create(userLogin, passwordHash);

            var safeUserName = GetSafeUserLogin(userLogin);

            _userCredentials.AddOrUpdate(
                safeUserName,
                userCredential,
                (login, credential) => userCredential);
        }

        [NotNull]
        public UserCredential Get([NotNull] string userLogin)
        {
            var safeUserName = GetSafeUserLogin(userLogin);

            if (!_userCredentials.TryGetValue(safeUserName, out var userCredential))
            {
                throw new ArgumentException("Unknown user login.");
            }

            return userCredential;
        }

        [NotNull]
        private static string GetSafeUserLogin([NotNull] string userLogin)
        {
            if (userLogin == null) throw new ArgumentNullException(nameof(userLogin));
            
            return userLogin.ToLower();
        }
    }
}