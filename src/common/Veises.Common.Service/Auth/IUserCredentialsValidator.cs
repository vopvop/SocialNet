namespace Veises.Common.Service.Auth
{
    public interface IUserCredentialsValidator
    {
        bool IsValid(IUserAuthData userAuthData);
    }
}