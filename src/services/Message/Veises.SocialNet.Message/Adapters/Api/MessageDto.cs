using System;

namespace Veises.SocialNet.Message.Adapters.Api
{
    public sealed class MessageDto
    {
        public DateTime CreatedUtc { get; set; }

        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime ModifiedUtc { get; set; }
    }
}