using System;
using Veises.Common.Extensions;
using Veises.Common.Service.Log;

namespace Veises.SocialNet.Message.Domaian
{
    internal sealed class Message : IEntity
    {
        private static readonly ILog Log = LogProvider.GetLogFor<Message>();

        private Message(Guid id, string content, DateTime createdUtc)
        {
            Id = id;
            Content = content ?? throw new ArgumentNullException(nameof(content));
            CreatedUtc = createdUtc;
            ModifiedUtc = CreatedUtc;
        }

        public DateTime CreatedUtc { get; }

        public DateTime ModifiedUtc { get; private set; }

        public string Content { get; private set; }

        public Guid Id { get; }

        public static Message Create(string content, DateTime createdUtc)
        {
            var id = Guid.NewGuid();

            var message = new Message(id, content, createdUtc);

            Log.WriteInfo($"Message {id.Escaped()} was created.");

            return message;
        }

        public void Update(string content, DateTime modifiedUtc)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));

            ModifiedUtc = modifiedUtc;

            Log.WriteInfo($"Message {Id.Escaped()} was updated.");
        }
    }
}