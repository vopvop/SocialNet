using System;
using System.Collections.Generic;
using System.Linq;
using Veises.Common.Service.IoC;
using Veises.SocialNet.Message.Adapters.Api;
using Veises.SocialNet.Message.Adapters.Database;

namespace Veises.SocialNet.Message.Services
{
    [InjectDependency(DependencyScope.Singleton)]
    internal sealed class MessageDao : IMessageDao
    {
        private readonly IRepository<Domaian.Message> _repository;

        public MessageDao(IRepository<Domaian.Message> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public MessageDto Get(Guid messageId)
        {
            var message = _repository.Get(messageId);

            return new MessageDto
            {
                Content = message.Content,
                Id = messageId
            };
        }

        public IEnumerable<MessageDto> GetAll()
        {
            return _repository.All().Select(m => new MessageDto
            {
                Content = m.Content,
                Id = m.Id
            });
        }
    }
}