using System.Collections.Generic;
using Veises.Common.Service.IoC;

namespace Veises.SocialNet.Message.Services
{
    [InjectDependency(DependencyScope.Singleton)]
    internal sealed class MessageDao : IMessageDao
    {
        public MessagePoco Get(string messageId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<MessagePoco> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}