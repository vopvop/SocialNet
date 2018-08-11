using System;
using System.Collections.Generic;
using System.Linq;
using Veises.Common.Service.IoC;
using Veises.SocialNet.Message.Adapters.Api;

namespace Veises.SocialNet.Message.Adapters.Database
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
                CreatedUtc = message.CreatedUtc,
                Id = messageId,
                ModifiedUtc = message.ModifiedUtc
            };
        }

        public IEnumerable<MessageDto> GetAll()
        {
            return _repository
                .All()
                .Select(m => new MessageDto
                {
                    Content = m.Content,
                    CreatedUtc = m.CreatedUtc,
                    Id = m.Id,
                    ModifiedUtc = m.ModifiedUtc
                });
        }
    }
}