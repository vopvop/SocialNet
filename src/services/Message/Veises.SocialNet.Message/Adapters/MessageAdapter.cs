using System;
using System.Collections.Generic;
using Veises.Common.Service.IoC;
using Veises.SocialNet.Message.Services;

namespace Veises.SocialNet.Message.Adapters
{
    [InjectDependency(DependencyScope.Singleton)]
    internal sealed class MessageAdapter : IMessageAdapter
    {
        private readonly IMessageService _messageService;

        private readonly IMessageDao _messageDao;

        public MessageAdapter(IMessageService messageService, IMessageDao messageDao)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _messageDao = messageDao ?? throw new ArgumentNullException(nameof(messageDao));
        }

        public void Delete(MessageIdDto id)
        {
            throw new System.NotImplementedException();
        }

        public MessageIdDto Post(string content)
        {
            throw new System.NotImplementedException();
        }

        public void Update(MessageIdDto id, string content)
        {
            throw new System.NotImplementedException();
        }

        public MessageDto Get(MessageIdDto messageId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<MessageDto> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}