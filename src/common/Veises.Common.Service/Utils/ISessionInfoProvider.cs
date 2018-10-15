using JetBrains.Annotations;

namespace Veises.Common.Service.Utils
{
    public interface ISessionInfoProvider
    {
        [NotNull]
        string GetSessionId();
    }
}