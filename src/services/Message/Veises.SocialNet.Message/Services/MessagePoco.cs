using System;

namespace Veises.SocialNet.Message.Services
{
    internal sealed class MessagePoco
    {
        public MessagePoco(string id, string content)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public string Id { get; }
        
        public string Content { get; }
    }
}