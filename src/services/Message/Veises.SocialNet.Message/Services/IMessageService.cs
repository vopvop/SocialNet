namespace Veises.SocialNet.Message.Services
{
    internal interface IMessageService
    {
        string Post(string content);

        void Update(string id, string message);
    }
}