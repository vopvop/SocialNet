using Veises.Common.Service.IoC;

namespace Veises.SocialNet.Message.Services
{
    [InjectDependency(DependencyScope.Singleton)]
    internal sealed class MessageService : IMessageService
    {
        public string Post(string content)
        {
            throw new System.NotImplementedException();
        }

        public void Update(string id, string message)
        {
            throw new System.NotImplementedException();
        }
    }
}