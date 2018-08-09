using System;

namespace Veises.SocialNet.Message.Adapters
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