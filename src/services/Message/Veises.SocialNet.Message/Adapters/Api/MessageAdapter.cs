using System;
using System.Collections.Generic;
using Veises.Common.Service.IoC;
using Veises.SocialNet.Message.Adapters.Database;
using Veises.SocialNet.Message.Services;

namespace Veises.SocialNet.Message.Adapters.Api
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

        public void Delete(Guid id)
        {
            _messageService.Delete(id);
        }

        public Guid Post(string content)
        {
            return _messageService.Post(content);
        }

        public void Update(Guid id, string content)
        {
            _messageService.Update(id, content);
        }

        public MessageDto Get(Guid messageId)
        {
            return _messageDao.Get(messageId);
        }

        public IEnumerable<MessageDto> GetAll()
        {
            return _messageDao.GetAll();
        }
    }
}