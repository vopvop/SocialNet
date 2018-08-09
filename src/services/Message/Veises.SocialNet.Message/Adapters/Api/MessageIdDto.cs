using System;

namespace Veises.SocialNet.Message.Adapters.Api
{
    public sealed class MessageIdDto
    {
        public MessageIdDto(Guid messageUid)
        {
            MessageUid = messageUid;
        }

        public Guid MessageUid { get; set; }
    }
}