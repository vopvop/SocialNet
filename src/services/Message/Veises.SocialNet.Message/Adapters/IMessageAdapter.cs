using System.Collections.Generic;

namespace Veises.SocialNet.Message.Adapters
{
    public interface IMessageAdapter
    {
        void Delete(MessageIdDto id);

        MessageIdDto Post(string content);

        void Update(MessageIdDto id, string content);

        MessageDto Get(MessageIdDto messageId);

        IEnumerable<MessageDto> GetAll();
    }
}