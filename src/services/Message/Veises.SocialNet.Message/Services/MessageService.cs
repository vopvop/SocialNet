using System;
using Veises.Common.Service.IoC;
using Veises.Common.Service.Utils;
using Veises.SocialNet.Message.Adapters.Database;

namespace Veises.SocialNet.Message.Services
{
    [InjectDependency(DependencyScope.Singleton)]
    internal sealed class MessageService : IMessageService
    {
        private readonly IRepository<Domaian.Message> _repository;

        private readonly ITimeService _timeService;

        public MessageService(IRepository<Domaian.Message> repository, ITimeService timeService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
        }

        public Guid Post(string content)
        {
            var message = Domaian.Message.Create(content, _timeService.GetCurrentUtc());

            _repository.Add(message);

            return message.Id;
        }

        public void Update(Guid id, string content)
        {
            var message = _repository.Get(id);

            message.Update(content, _timeService.GetCurrentUtc());

            _repository.Update(message);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }
    }
}