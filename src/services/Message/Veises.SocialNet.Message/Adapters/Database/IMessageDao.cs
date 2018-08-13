using System;
using System.Collections.Generic;
using Veises.SocialNet.Message.Adapters.Api;

namespace Veises.SocialNet.Message.Adapters.Database
{
    internal interface IMessageDao
    {
        MessageDto Get(Guid messageId);

        IEnumerable<MessageDto> GetAll();
    }
}