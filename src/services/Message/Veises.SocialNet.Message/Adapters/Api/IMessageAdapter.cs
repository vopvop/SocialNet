using System;
using System.Collections.Generic;

namespace Veises.SocialNet.Message.Adapters.Api
{
    public interface IMessageAdapter
    {
        void Delete(Guid id);

        Guid Post(string content);

        void Update(Guid id, string content);

        MessageDto Get(Guid messageId);

        IEnumerable<MessageDto> GetAll();
    }
}