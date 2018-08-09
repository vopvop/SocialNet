using System;
using Veises.Common.Extensions;
using Veises.Common.Service.Log;

namespace Veises.SocialNet.Message.Domaian
{
    internal sealed class Message : IEntity
    {
        private static readonly ILogFor<Message> Log = LogServiceProvider.GetLogFor<Message>();
        
        public Guid Id { get; private set; }
        
        public string Content { get; private set; }
        
        private Message(Guid id, string content)
        {
            Id = id;
            Content = content;
        }

        public static Message Create(string content)
        {
            var id = Guid.NewGuid();
            
            var message =  new Message(id, content);
            
            Log.WriteInfo($"Message {id.Escaped()} was created.");

            return message;
        }
        
        public void Update(string content)
        {
            Content = content;
            
            Log.WriteInfo($"Message {Id.Escaped()} was updated.");
        }
    }
}