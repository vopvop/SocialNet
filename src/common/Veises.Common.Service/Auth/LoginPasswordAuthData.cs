using System;

namespace Veises.Common.Service.Auth
{
    public class LoginPasswordAuthData : IUserAuthData
    {
        public string Login { get; }

        public string PasswordHash { get; }

        public LoginPasswordAuthData(string login, string passwordHash)
        {
            Login = login ?? throw new ArgumentNullException(nameof(login));
            PasswordHash = passwordHash;
        }

        public string GetUserSystemName() => Login;
    }
}