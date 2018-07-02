namespace Veises.Common.Service.Auth
{
    public interface IAuthService
    {
        UserInfo GetUserInfo();

        void Authorize(IUserAuthData userAuthData);
    }
}