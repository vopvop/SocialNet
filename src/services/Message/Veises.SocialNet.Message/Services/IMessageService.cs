using System;

namespace Veises.SocialNet.Message.Services
{
    internal interface IMessageService
    {
        Guid Post(string content);

        void Update(Guid id, string message);

        void Delete(Guid id);
    }
}