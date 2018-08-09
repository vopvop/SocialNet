using System;

namespace Veises.SocialNet.Message.Adapters.Api
{
    public sealed class MessageDto
    {
        public Guid Id { get; set; }

        public string Content { get; set; }
    }
}