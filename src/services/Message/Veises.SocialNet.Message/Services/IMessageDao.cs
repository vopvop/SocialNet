using System.Collections.Generic;

namespace Veises.SocialNet.Message.Services
{
    internal interface IMessageDao
    {
        MessagePoco Get(string messageId);

        IEnumerable<MessagePoco> GetAll();
    }
}